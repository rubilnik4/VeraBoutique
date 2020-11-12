using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base.DatabaseTable;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Implementations.ResultFactory;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.Base
{
    /// <summary>
    /// Базовый сервис получения данных из базы
    /// </summary>
    public abstract class DatabaseService<TId, TDomain, TEntityIn, TEntityOut> : IDatabaseService<TId, TDomain>
       where TDomain : IDomainModel<TId>
       where TEntityIn : IEntityModel<TId>
       where TEntityOut : class, TEntityIn
       where TId : notnull
    {
        protected DatabaseService(IDatabase database,
                                  IDatabaseTable<TId, TDomain, TEntityOut> dataTable,
                                  IEntityConverter<TId, TDomain, TEntityIn, TEntityOut> entityConverter)
        {
            _database = database;
            _dataTable = dataTable;
            _entityConverter = entityConverter;
        }

        /// <summary>
        /// База данных
        /// </summary>
        private readonly IDatabase _database;

        /// <summary>
        /// Таблица базы данных
        /// </summary>
        private readonly IDatabaseTable<TId, TDomain, TEntityOut> _dataTable;

        /// <summary>
        /// Конвертер из доменной модели в модель базы данных
        /// </summary>
        private readonly IEntityConverter<TId, TDomain, TEntityIn, TEntityOut> _entityConverter;

        /// <summary>
        /// Получить модели из базы
        /// </summary>
        public async Task<IResultCollection<TDomain>> Get() =>
            await _dataTable.ToListAsync().
            ResultCollectionBindOkTaskAsync(entities => _entityConverter.FromEntities(entities));

        /// <summary>
        /// Получить модель из базы по идентификатору
        /// </summary>
        public async Task<IResultValue<TDomain>> Get(TId id) =>
            await _dataTable.FindIdAsync(id).
            ResultValueBindOkTaskAsync(entity => _entityConverter.FromEntity(entity));

        /// <summary>
        /// Загрузить модель в базу
        /// </summary>
        public virtual async Task<IResultValue<TId>> Post(TDomain model) =>
            await _dataTable.FindIdAsync(model.Id).
            ResultValueBindWhereBindAsync(entity => entity == null,
                okFunc: _ => AddWithSaving(_dataTable, model),
                badFunc: entity => GetDuplicateErrorResult(entity, _dataTable.TableName));

        /// <summary>
        /// Загрузить модели в базу
        /// </summary>
        public virtual async Task<IResultCollection<TId>> Post(IEnumerable<TDomain> models) =>
            await PostCollection(models.ToList());

        /// <summary>
        /// Загрузить модели в базу
        /// </summary>
        private async Task<IResultCollection<TId>> PostCollection(IReadOnlyCollection<TDomain> models) =>
            await _dataTable.FindIdsAsync(models.Select(model => model.Id)).
            ResultCollectionBindWhereBindAsync(entities => entities.Count == 0,
                okFunc: _ => AddRangeWithSaving(_dataTable, models),
                badFunc: ids => GetDuplicateErrorsResult(ids, _dataTable.TableName));

        /// <summary>
        /// Заменить модель в базе по идентификатору
        /// </summary>
        public async Task<IResultError> Put(TDomain model) =>
            await _dataTable.FindIdAsync(model.Id).
            ResultValueBindErrorsOkTaskAsync(_ => _dataTable.Update(_entityConverter.ToEntity(model))).
            ToResultErrorTaskAsync().
            ResultErrorBindOkBindAsync(DatabaseSaveChanges);

        /// <summary>
        /// Удалить модель из базы по идентификатору
        /// </summary>
        public async Task<IResultValue<TDomain>> Delete(TId id) =>
            await _dataTable.FindIdAsync(id).
            ResultValueBindErrorsOkTaskAsync(entity => _dataTable.Remove(entity)).
            ResultValueBindOkTaskAsync(entity => _entityConverter.FromEntity(entity)).
            ResultValueBindErrorsOkBindAsync(_ => DatabaseSaveChanges());

        /// <summary>
        /// Проверить наличие моделей 
        /// </summary>
        public async Task<IResultError> Validate(TDomain model) =>
            await ValidateValue(model);

        /// <summary>
        /// Проверить наличие моделей 
        /// </summary>
        public async Task<IResultError> Validate(IEnumerable<TDomain> models) =>
            await ValidateCollection(models.ToList());

        /// <summary>
        /// Функция выбора сущностей для проверки наличия
        /// </summary>
        protected virtual IQueryable<TEntityOut> ValidateFilter(IQueryable<TEntityOut> entities, TDomain domain) =>
           entities.Where(_dataTable.ValidateByDomain(domain));

        /// <summary>
        /// Функция выбора сущностей для проверки наличия
        /// </summary>
        protected virtual IQueryable<TEntityOut> ValidateFilter(IQueryable<TEntityOut> entities, 
                                                                IReadOnlyCollection<TDomain> domains) =>
           entities.Where(_dataTable.ValidateByDomains(domains));

        /// <summary>
        /// Добавить модель в базу и сохранить
        /// </summary>
        private async Task<IResultValue<TId>> AddWithSaving(IDatabaseTable<TId, TDomain, TEntityOut> dataTable,
                                                                 TDomain model) =>
            await dataTable.AddAsync(_entityConverter.ToEntity(model)).
            ResultValueBindErrorsOkBindAsync(_ => DatabaseSaveChanges());

        /// <summary>
        /// Добавить модели в базу и сохранить
        /// </summary>
        private async Task<IResultCollection<TId>> AddRangeWithSaving(IDatabaseTable<TId, TDomain, TEntityOut> dataTable,
                                                                      IEnumerable<TDomain> models) =>
            await dataTable.AddRangeAsync(_entityConverter.ToEntities(models)).
            ResultCollectionBindErrorsOkBindAsync(_ => DatabaseSaveChanges());

        /// <summary>
        /// Сохранить изменения в базе или вернуть ошибки
        /// </summary>
        private async Task<IResultError> DatabaseSaveChanges() =>
            await _database.SaveChangesAsync();

        /// <summary>
        /// Получить ошибку двойной записи
        /// </summary>
        private static async Task<IResultValue<TId>> GetDuplicateErrorResult(TEntityOut id, string tableName) =>
            await ResultValueFactory.
            CreateTaskResultValueError<TId>(DatabaseErrors.DuplicateError(id, tableName));

        /// <summary>
        /// Получить ошибки двойной записи
        /// </summary>
        private static async Task<IResultCollection<TId>> GetDuplicateErrorsResult(IEnumerable<TEntityOut> ids, string tableName) =>
            await ResultCollectionFactory.
            CreateTaskResultCollectionError<TId>(DatabaseErrors.DuplicateErrors(ids, tableName));

        /// <summary>
        /// Проверить наличие модели
        /// </summary>
        private async Task<IResultError> ValidateValue(TDomain model) =>
            await _dataTable.FindExpressionAsync(ValidateQuery(model));

        /// <summary>
        /// Проверить наличие коллекции моделей 
        /// </summary>
        private async Task<IResultError> ValidateCollection(IReadOnlyCollection<TDomain> models) =>
            await _dataTable.FindsExpressionAsync(ValidateQuery(models)).
            ResultCollectionOkTaskAsync(ids => models.Where(model => !ids.Contains(model.Id))).
            ResultCollectionBindErrorsOkTaskAsync(entitiesNotFound =>
                entitiesNotFound.
                Select(entityNotFound => DatabaseErrors.ValueNotFoundError(entityNotFound.Id?.ToString() ?? String.Empty,
                                                                           _dataTable.GetType().Name)).
                Map(errors => new ResultError(errors)));

        /// <summary>
        /// Запрос проверки наличия сущностей
        /// </summary>
        private Func<IQueryable<TEntityOut>, IQueryable<TId>> ValidateQuery(TDomain domain) =>
            entities => ValidateFilter(entities, domain).Select(_dataTable.IdSelect());

        /// <summary>
        /// Запрос проверки наличия сущностей
        /// </summary>
        private Func<IQueryable<TEntityOut>, IQueryable<TId>> ValidateQuery(IReadOnlyCollection<TDomain> domains) =>
            entities => ValidateFilter(entities, domains).Select(_dataTable.IdSelect());
    }
}