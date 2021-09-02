using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using Microsoft.EntityFrameworkCore;
using static ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections.ResultCollectionTryAsyncExtensions;
using static ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues.ResultValueTryAsyncExtensions;
using static ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues.ResultValueTryExtensions;
using static ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues.ResultValueBindTryAsyncExtensions;
using static ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors.ResultErrorTryExtensions;
using static ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors.ResultErrorTryAsyncExtensions;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Base.EntityDatabaseTable
{
    /// <summary>
    /// Таблица базы данных EntityFramework. Функции нахождения данных
    /// </summary>
    public abstract partial class EntityDatabaseTable<TId, TDomain, TEntity>
        where TDomain : IDomainModel<TId>
        where TEntity : class, IEntityModel<TId>
        where TId : notnull
    {
        /// <summary>
        /// Вернуть запись из таблицы по идентификатору асинхронно
        /// </summary>
        public async Task<IResultValue<TEntity>> FindByIdAsync(TId id) =>
            await FindAsyncWrapper(() => _databaseSet.AsNoTracking().FirstOrDefaultAsync(IdPredicate(id))!, id);

        /// <summary>
        /// Вернуть полную запись из таблицы по идентификатору асинхронно
        /// </summary>
        public async Task<IResultValue<TEntity>> FindMainByIdAsync(TId id) =>
            await FindAsyncWrapper(() => EntitiesIncludes.AsNoTracking().FirstOrDefaultAsync(IdPredicate(id))!, id);

        /// <summary>
        /// Найти записи в таблице по идентификаторам
        /// </summary>
        public async Task<IResultCollection<TEntity>> FindByIdsAsync(IEnumerable<TId> ids) =>
            await FindEntityAsync(() => _databaseSet.AsNoTracking().Where(IdsPredicate(ids)).ToListAsync());

        /// <summary>
        /// Вернуть полные записи из таблицы по идентификаторам асинхронно
        /// </summary>
        public async Task<IResultCollection<TEntity>> FindMainByIdsAsync(IEnumerable<TId> ids) =>
            await FindEntityAsync(() => EntitiesIncludes.AsNoTracking().Where(IdsPredicate(ids)).ToListAsync());

        /// <summary>
        /// Выполнить запрос в таблице и выгрузить сущности
        /// </summary>
        public async Task<IResultValue<TOut>> FindExpressionValueAsync<TOut>(Func<IQueryable<TEntity>, Task<TOut>> queryFunc, TId id)
            where TOut : notnull =>
            await ResultValueBindTryAsync(() => queryFunc(_databaseSet.AsNoTracking()).
                                                ToResultValueNullValueCheckTaskAsync(DatabaseErrors.ValueNotFoundError(id.ToString()!, TableName)),
                                          TableAccessErrorType);

        /// <summary>
        /// Выполнить запрос в таблице и выгрузить сущности
        /// </summary>
        public async Task<IResultValue<TEntityOut>> FindExpressionAsync<TEntityOut>(Func<IQueryable<TEntity>, Task<TEntityOut?>> queryFunc, TId id)
            where TEntityOut : class, IEntityModel<TId> =>
            await ResultValueBindTryAsync(() => queryFunc(_databaseSet.AsNoTracking()).
                                                ToResultValueNullCheckTaskAsync(DatabaseErrors.ValueNotFoundError(id.ToString()!, TableName)),
                                          TableAccessErrorType);

        /// <summary>
        /// Выполнить запрос в таблице и выгрузить сущности
        /// </summary>
        public async Task<IResultCollection<TOut>> FindsExpressionValueAsync<TOut>(Func<IQueryable<TEntity>, IQueryable<TOut>> queryFunc)
             where TOut : notnull =>
            await ResultCollectionTryAsync(() => queryFunc(_databaseSet.AsNoTracking()).ToListAsync(), TableAccessErrorType);

        /// <summary>
        /// Выполнить запрос в таблице и выгрузить сущности
        /// </summary>
        public async Task<IResultCollection<TEntityOut>> FindsExpressionAsync<TEntityOut>(Func<IQueryable<TEntity>, IQueryable<TEntityOut>> queryFunc)
            where TEntityOut : class, IEntityModel<TId> =>
            await ResultCollectionTryAsync(() => queryFunc(_databaseSet.AsNoTracking()).ToListAsync(), TableAccessErrorType);

        /// <summary>
        /// Выгрузка сущностей с включенными данными
        /// </summary>
        protected virtual IQueryable<TEntity> EntitiesIncludes =>
            _databaseSet;

        /// <summary>
        /// Выгрузка сущностей с включенными данными при удалении
        /// </summary>
        protected virtual IQueryable<TEntity> EntitiesIncludesDelete =>
            _databaseSet;

        /// <summary>
        /// Поиск элемента с проверкой
        /// </summary>
        private async Task<IResultValue<TEntity>> FindAsyncWrapper(Func<Task<TEntity?>> getEntity, TId id) =>
            await ResultValueBindTryAsync(() => getEntity()!.
                                                ToResultValueNullCheckTaskAsync(DatabaseErrors.ValueNotFoundError(id.ToString()!, TableName)),
                                          TableAccessErrorType);

        /// <summary>
        /// Найти сущности в таблице по идентификаторам
        /// </summary>
        private async Task<IResultCollection<TEntity>> FindEntityAsync(Func<Task<List<TEntity>>> getEntities) =>
            await ResultCollectionTryAsync(getEntities, TableAccessErrorType);
    }
}