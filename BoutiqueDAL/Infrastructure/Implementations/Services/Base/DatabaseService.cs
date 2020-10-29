using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
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
                                  IDatabaseTable<TId, TEntityOut> dataTable,
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
        private readonly IDatabaseTable<TId, TEntityOut> _dataTable;

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
            await _dataTable.FindAsync(id).
            ResultValueBindOkTaskAsync(entity => _entityConverter.FromEntity(entity));

        /// <summary>
        /// Загрузить модели в базу
        /// </summary>
        public virtual async Task<IResultCollection<TId>> Post(IReadOnlyCollection<TDomain> models) =>
            await _dataTable.FindAsync(models.Select(model => model.Id)).
            ResultCollectionBindWhereBindAsync(entities => entities.Count == 0,
                okFunc: _ => AddRangeWithSaving(_dataTable, models),
                badFunc: ids => GetDuplicateErrorResult(ids, _dataTable.TableName));

        /// <summary>
        /// Заменить модель в базе по идентификатору
        /// </summary>
        public async Task<IResultError> Put(TDomain model) =>
            await _dataTable.FindAsync(model.Id).
            ResultValueBindErrorsOkTaskAsync(_ => _dataTable.Update(_entityConverter.ToEntity(model))).
            ToResultErrorTaskAsync().
            ResultErrorBindOkBindAsync(DatabaseSaveChanges);

        /// <summary>
        /// Удалить модель из базы по идентификатору
        /// </summary>
        public async Task<IResultValue<TDomain>> Delete(TId id) =>
            await _dataTable.FindAsync(id).
            ResultValueBindErrorsOkTaskAsync(entity => _dataTable.Remove(entity)).
            ResultValueBindOkTaskAsync(entity => _entityConverter.FromEntity(entity)).
            ResultValueBindErrorsOkBindAsync(_ => DatabaseSaveChanges());

        /// <summary>
        /// Проверить наличие моделей
        /// </summary>
        public virtual async Task<IResultError> CheckEntities(IEnumerable<TDomain> models) =>
            await CheckEntitiesCollection(models.Distinct().ToList());

        /// <summary>
        /// Добавить модели в базу и сохранить
        /// </summary>
        private async Task<IResultCollection<TId>> AddRangeWithSaving(IDatabaseTable<TId, TEntityOut> dataTable,
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
        private static async Task<IResultCollection<TId>> GetDuplicateErrorResult(IEnumerable<TEntityOut> ids, string tableName) =>
            await ResultCollectionFactory.
            CreateTaskResultCollectionError<TId>(DatabaseErrors.DuplicateError(ids, tableName));

        /// <summary>
        /// Проверить наличие моделей
        /// </summary>
        private async Task<IResultError> CheckEntitiesCollection(IReadOnlyCollection<TDomain> models) =>
            await _dataTable.FindAsync(models.Select(model => model.Id)).
            ResultCollectionOkTaskAsync(entities => models.Where(model => entities.All(entity => !entity.Id.Equals(model.Id)))).
            ResultCollectionBindErrorsOkTaskAsync(entitiesNotFound =>
                entitiesNotFound.
                Select(entityNotFound => DatabaseErrors.ValueNotFoundError(entityNotFound.Id?.ToString() ?? String.Empty,
                                                                           _dataTable.GetType().Name)).
                Map(errors => new ResultError(errors)));
    }
}