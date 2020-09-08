using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDAL.Factories.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.Base
{
    /// <summary>
    /// Базовый сервис получения данных из базы
    /// </summary>
    public abstract class DatabaseService<TId, TDomain, TEntity> : IDatabaseService<TId, TDomain>
        where TDomain : IDomainModel<TId>
        where TEntity : IEntityModel<TId>
        where TId: notnull
    {
        protected DatabaseService(IResultValue<IDatabase> database,
                                  IResultValue<IDatabaseTable<TId, TEntity>> dataTable,
                                  IEntityConverter<TId, TDomain, TEntity> entityConverter)
        {
            _database = database;
            _dataTable = dataTable;
            _entityConverter = entityConverter;
        }

        /// <summary>
        /// База данных
        /// </summary>
        private readonly IResultValue<IDatabase> _database;

        /// <summary>
        /// Таблица базы данных
        /// </summary>
        private readonly IResultValue<IDatabaseTable<TId, TEntity>> _dataTable;

        /// <summary>
        /// Конвертер из доменной модели в модель базы данных
        /// </summary>
        private readonly IEntityConverter<TId, TDomain, TEntity> _entityConverter;

        /// <summary>
        /// Получить модели из базы
        /// </summary>
        public async Task<IResultCollection<TDomain>> Get() =>
            await _dataTable.
            ResultValueBindOkToCollectionAsync(dataTable => dataTable.ToListAsync()).
            ResultCollectionOkTaskAsync(entities => _entityConverter.FromEntities(entities));

        /// <summary>
        /// Получить модель из базы по идентификатору
        /// </summary>
        public async Task<IResultValue<TDomain>> Get(TId id) =>
            await _dataTable.
            ResultValueBindOkAsync(dataTable => dataTable.FirstAsync(id)).
            ResultValueOkTaskAsync(entity => _entityConverter.FromEntity(entity));

        /// <summary>
        /// Загрузить модели в базу
        /// </summary>
        public async Task<IResultCollection<TId>> Post(IEnumerable<TDomain> models) =>
            await _dataTable.
            ResultValueBindOkToCollectionAsync(dataTable => dataTable.AddRangeAsync(_entityConverter.ToEntities(models))).
            ResultCollectionBindErrorsOkBindAsync(_ => DatabaseSaveChanges());

        /// <summary>
        /// Заменить модель в базе по идентификатору
        /// </summary>
        public async Task<IResultError> Put(TId id, TDomain model) =>
            await _dataTable.
            ResultValueBindErrorsOk(dataTable => dataTable.Update(_entityConverter.ToEntity(model))).
            ResultErrorBindOkAsync(DatabaseSaveChanges);

        /// <summary>
        /// Удалить модель из базы по идентификатору
        /// </summary>
        public async Task<IResultValue<TDomain>> Delete(TId id) =>
            await _dataTable.
            ResultValueBindOk(dataTable => dataTable.Remove(CreateRemoveEntityById(id))).
            ResultValueOk(entity => _entityConverter.FromEntity(entity)).
            ResultValueBindErrorsOkAsync(_ => DatabaseSaveChanges());

        /// <summary>
        /// Создать модель базы данных для удаления по идентификатору
        /// </summary>
        protected abstract TEntity CreateRemoveEntityById(TId id);

        /// <summary>
        /// Сохранить изменения в базе или вернуть ошибки
        /// </summary>
        private async Task< IResultError> DatabaseSaveChanges() =>
            await _database.
            ResultValueOkAsync(database => database.SaveChangesAsync()).
            MapTaskAsync(resultDatabase => (IResultError)resultDatabase);
    }
}