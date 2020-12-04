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
                                  IDatabaseValidateService<TId, TDomain> databaseValidateService,
                                  IEntityConverter<TId, TDomain, TEntityOut> entityConverter)
        {
            _database = database;
            _dataTable = dataTable;
            _databaseValidateService = databaseValidateService;
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
        /// Базовый сервис проверки данных из базы
        /// </summary>
        private readonly IDatabaseValidateService<TId, TDomain> _databaseValidateService;

        /// <summary>
        /// Конвертер из доменной модели в модель базы данных
        /// </summary>
        private readonly IEntityConverter<TId, TDomain, TEntityOut> _entityConverter;

        /// <summary>
        /// Получить полные модели из базы
        /// </summary>
        public async Task<IResultCollection<TDomain>> Get() =>
            await _dataTable.ToListAsync().
            ResultCollectionBindOkTaskAsync(entities => _entityConverter.FromEntities(entities));

        /// <summary>
        /// Получить  полную модель из базы по идентификатору
        /// </summary>
        public async Task<IResultValue<TDomain>> Get(TId id) =>
            await _dataTable.FindIdAsync(id).
            ResultValueBindOkTaskAsync(entity => _entityConverter.FromEntity(entity));

        /// <summary>
        /// Загрузить модель в базу
        /// </summary>
        public async Task<IResultValue<TId>> Post(TDomain domain) =>
            await new ResultValue<TDomain>(domain).
            ResultValueBindErrorsOkAsync(_ => _databaseValidateService.Validate(domain)).
            ResultValueBindOkBindAsync(_ => AddWithSaving(_dataTable, domain));

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
            ResultCollectionBindErrorsOkAsync(_ => _databaseValidateService.Validate(domains)).
            ResultCollectionBindOkBindAsync(_ => AddRangeWithSaving(_dataTable, domains));

        /// <summary>
        /// Заменить модель в базе по идентификатору
        /// </summary>
        public async Task<IResultError> Put(TDomain domain) =>
            await _databaseValidateService.ValidateFind(domain.Id).
            ResultErrorBindOkBindAsync(() => _databaseValidateService.Validate(domain)).
            ResultErrorBindOkTaskAsync(() => _dataTable.Update(_entityConverter.ToEntity(domain))).
            ResultErrorBindOkBindAsync(DatabaseSaveChanges);

        /// <summary>
        /// Удалить модель из базы по идентификатору
        /// </summary>
        public async Task<IResultValue<TDomain>> Delete(TId id) =>
            await _dataTable.FindShortIdAsync(id).
            ResultValueBindErrorsOkTaskAsync(entity => _dataTable.Remove(entity)).
            ResultValueBindOkTaskAsync(entity => _entityConverter.FromEntity(entity)).
            ResultValueBindErrorsOkBindAsync(_ => DatabaseSaveChanges());
        
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


    }
}