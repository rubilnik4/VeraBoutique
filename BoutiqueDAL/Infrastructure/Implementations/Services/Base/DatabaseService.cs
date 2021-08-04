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
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Implementations.ResultFactory;
using Functional.Models.Interfaces.Result;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.Base
{
    /// <summary>
    /// Базовый сервис получения данных из базы
    /// </summary>
    public abstract class DatabaseService<TId, TDomain, TEntity> : IDatabaseService<TId, TDomain>
       where TDomain : IDomainModel<TId>
       where TEntity : class, IEntityModel<TId>
       where TId : notnull
    {
        protected DatabaseService(IDatabase database,
                                  IDatabaseTable<TId, TDomain, TEntity> dataTable,
                                  IDatabaseValidateService<TId, TDomain> databaseValidateService,
                                  IEntityConverter<TId, TDomain, TEntity> mainEntityConverter)
        {
            _database = database;
            _dataTable = dataTable;
            _databaseValidateService = databaseValidateService;
            _mainEntityConverter = mainEntityConverter;
        }

        /// <summary>
        /// База данных
        /// </summary>
        private readonly IDatabase _database;

        /// <summary>
        /// Таблица базы данных
        /// </summary>
        private readonly IDatabaseTable<TId, TDomain, TEntity> _dataTable;

        /// <summary>
        /// Базовый сервис проверки данных из базы
        /// </summary>
        private readonly IDatabaseValidateService<TId, TDomain> _databaseValidateService;

        /// <summary>
        /// Конвертер из доменной модели в модель базы данных
        /// </summary>
        private readonly IEntityConverter<TId, TDomain, TEntity> _mainEntityConverter;

        /// <summary>
        /// Получить полные модели из базы
        /// </summary>
        public async Task<IResultCollection<TDomain>> Get() =>
            await _dataTable.ToListMainAsync().
            ResultCollectionBindOkTaskAsync(entities => _mainEntityConverter.FromEntities(entities));

        /// <summary>
        /// Получить  полную модель из базы по идентификатору
        /// </summary>
        public async Task<IResultValue<TDomain>> Get(TId id) =>
            await _dataTable.FindMainByIdAsync(id).
            ResultValueBindOkTaskAsync(entity => _mainEntityConverter.FromEntity(entity));

        /// <summary>
        /// Загрузить модель в базу
        /// </summary>
        public async Task<IResultValue<TId>> Post(TDomain domain) =>
            await new ResultValue<TDomain>(domain).
            ResultValueBindErrorsOkAsync(_ => _databaseValidateService.ValidatePost(domain)).
            ResultValueBindOkBindAsync(_ => AddWithSaving(domain));

        /// <summary>
        /// Загрузить модели в базу
        /// </summary>
        public async Task<IResultCollection<TId>> Post(IEnumerable<TDomain> models) =>
            await PostCollection(models.ToList());

        /// <summary>
        /// Загрузить модели в базу
        /// </summary>
        private async Task<IResultCollection<TId>> PostCollection(IReadOnlyCollection<TDomain> domains) =>
            await new ResultCollection<TDomain>(domains).
            ResultCollectionBindErrorsOkAsync(_ => _databaseValidateService.ValidatePost(domains)).
            ResultCollectionBindOkBindAsync(_ => AddRangeWithSaving(_dataTable, domains));

        /// <summary>
        /// Заменить модель в базе по идентификатору
        /// </summary>
        public async Task<IResultError> Put(TDomain domain) =>
            await _databaseValidateService.ValidatePut(domain).
            ResultErrorBindOkBindAsync(() => UpdateWithSaving(domain));

        /// <summary>
        /// Удалить все модели из базы
        /// </summary>
        public async Task<IResultError> Delete() =>
            await _dataTable.Remove().
            ResultErrorBindOkAsync(DatabaseSaveChanges);

        /// <summary>
        /// Удалить модель из базы по идентификатору
        /// </summary>
        public async Task<IResultValue<TDomain>> Delete(TId id) =>
            await _dataTable.FindExpressionAsync(entities => entities.FirstOrDefaultAsync(_dataTable.IdPredicate(id)).
                                                             MapTaskAsync(entity => (TEntity?)entity),
                                                 id).
            ResultValueBindOkBindAsync(DeleteWithSaving);

        /// <summary>
        /// Добавить модель в базу и сохранить
        /// </summary>
        private async Task<IResultValue<TId>> AddWithSaving(TDomain model) =>
            await _dataTable.AddAsync(_mainEntityConverter.ToEntity(model)).
            ResultValueBindErrorsOkBindAsync(_ => DatabaseSaveChanges());

        /// <summary>
        /// Добавить модели в базу и сохранить
        /// </summary>
        private async Task<IResultCollection<TId>> AddRangeWithSaving(IDatabaseTable<TId, TDomain, TEntity> dataTable,
                                                                      IEnumerable<TDomain> models) =>
            await dataTable.AddRangeAsync(_mainEntityConverter.ToEntities(models)).
            ResultCollectionBindErrorsOkBindAsync(_ => DatabaseSaveChanges());

        /// <summary>
        /// Добавить модель в базу и сохранить
        /// </summary>
        private async Task<IResultError> UpdateWithSaving(TDomain domain) =>
            await _dataTable.Update(_mainEntityConverter.ToEntity(domain)).
            ResultErrorBindOkAsync(DatabaseSaveChanges);

        /// <summary>
        /// Добавить модель в базу и сохранить
        /// </summary>
        private async Task<IResultValue<TDomain>> DeleteWithSaving(TEntity entity) =>
            await _dataTable.Remove(entity).
            ResultValueBindOk(_mainEntityConverter.FromEntity).
            ResultValueBindErrorsOkAsync(_ => DatabaseSaveChanges());

        /// <summary>
        /// Сохранить изменения в базе или вернуть ошибки
        /// </summary>
        private async Task<IResultError> DatabaseSaveChanges() =>
            await _database.SaveChangesAsync();
    }
}