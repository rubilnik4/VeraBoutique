using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Microsoft.EntityFrameworkCore;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection.ResultCollectionTryAsyncExtensions;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultValue.ResultValueTryAsyncExtensions;
using static Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue.ResultValueTryExtensions;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultValue.ResultValueBindTryAsyncExtensions;
using static Functional.FunctionalExtensions.Sync.ResultExtension.ResultError.ResultErrorTryExtensions;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultError.ResultErrorTryAsyncExtensions;

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
        public async Task<IResultValue<TEntity>> FindShortIdAsync(TId id) =>
            await FindAsyncWrapper(() => _databaseSet.AsNoTracking().FirstOrDefaultAsync(IdPredicate(id))!, id);

        /// <summary>
        /// Вернуть полную запись из таблицы по идентификатору асинхронно
        /// </summary>
        public async Task<IResultValue<TEntity>> FindIdAsync(TId id) =>
            await FindAsyncWrapper(() => EntitiesIncludes.AsNoTracking().
                                         FirstOrDefaultAsync(IdPredicate(id))!,
                                   id);

        /// <summary>
        /// Найти записи в таблице по идентификаторам
        /// </summary>
        public async Task<IResultCollection<TEntity>> FindShortIdsAsync(IEnumerable<TId> ids) =>
            await FindEntityAsync(() => _databaseSet.AsNoTracking().Where(IdsPredicate(ids)).ToListAsync());

        /// <summary>
        /// Вернуть полные записи из таблицы по идентификаторам асинхронно
        /// </summary>
        public async Task<IResultCollection<TEntity>> FindIdsAsync(IEnumerable<TId> ids) =>
            await FindEntityAsync(() => EntitiesIncludes.AsNoTracking().Where(IdsPredicate(ids)).ToListAsync());

        /// <summary>
        /// Выполнить запрос в таблице и выгрузить сущности
        /// </summary>
        public async Task<IResultValue<TOut>> FindExpressionAsync<TOut>(Func<IQueryable<TEntity>, IQueryable<TOut>> queryFunc, TId id)
            where TOut : notnull =>
            await ResultValueBindTryAsync(() => queryFunc(_databaseSet.AsNoTracking()).ToListAsync().
                                                ToResultValueWhereTaskAsync(ids => ids.Count > 0,
                                                                            _ => DatabaseErrors.ValueNotFoundError(id.ToString()!, TableName)).
                                                ResultValueOkTaskAsync(ids => ids.First()), 
                                          TableAccessError);

        /// <summary>
        /// Выполнить запрос в таблице и выгрузить сущности
        /// </summary>
        public async Task<IResultCollection<TOut>> FindsExpressionAsync<TOut>(Func<IQueryable<TEntity>, IQueryable<TOut>> queryFunc)
            where TOut : notnull =>
            await ResultCollectionTryAsync(() => queryFunc(_databaseSet.AsNoTracking()).ToListAsync(), TableAccessError);

        /// <summary>
        /// Выгрузка сущностей с включенными данными
        /// </summary>
        protected virtual IQueryable<TEntity> EntitiesIncludes =>
            _databaseSet;

        /// <summary>
        /// Поиск элемента с проверкой
        /// </summary>
        private async Task<IResultValue<TEntity>> FindAsyncWrapper(Func<Task<TEntity?>> getEntity, TId id) =>
            await ResultValueBindTryAsync(() => getEntity().
                                                ToResultValueNullCheckTaskAsync(DatabaseErrors.ValueNotFoundError(id.ToString()!, TableName)),
                                          TableAccessError);

        /// <summary>
        /// Найти сущности в таблице по идентификаторам
        /// </summary>
        private async Task<IResultCollection<TEntity>> FindEntityAsync(Func<Task<List<TEntity>>> getEntities) =>
            await ResultCollectionTryAsync(getEntities, TableAccessError);
    }
}