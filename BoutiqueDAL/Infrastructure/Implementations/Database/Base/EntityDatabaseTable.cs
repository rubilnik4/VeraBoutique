using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.Models.Interfaces.Result;
using Microsoft.EntityFrameworkCore;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection.ResultCollectionTryAsyncExtensions;
using static Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue.ResultValueTryExtensions;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultValue.ResultValueBindTryAsyncExtensions;
using static Functional.FunctionalExtensions.Sync.ResultExtension.ResultError.ResultErrorTryExtensions;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultError.ResultErrorTryAsyncExtensions;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Base
{
    /// <summary>
    /// Таблица базы данных EntityFramework
    /// </summary>
    public abstract class EntityDatabaseTable<TId, TEntity> : DbSet<TEntity>, IDatabaseTable<TId, TEntity>
        where TEntity : class, IEntityModel<TId>
        where TId : notnull
    {
        protected EntityDatabaseTable(DbSet<TEntity> databaseSet)
        {
            _databaseSet = databaseSet;
        }

        /// <summary>
        /// Экземпляр таблицы базы данных
        /// </summary>
        private readonly DbSet<TEntity> _databaseSet;

        /// <summary>
        /// Имя таблицы
        /// </summary>
        public string TableName => GetType().Name;

        /// <summary>
        /// Вернуть записи из таблицы асинхронно
        /// </summary>
        public async Task<IResultCollection<TEntity>> ToListAsync() =>
            await ResultCollectionTryAsync(() => _databaseSet.ToListAsync(), TableAccessError);

        /// <summary>
        /// Вернуть записи из таблицы асинхронно с включением сущностей
        /// </summary>
        public async Task<IResultCollection<TEntity>> ToListAsync<TIdOut, TEntityOut>(Expression<Func<TEntity, IEnumerable<TEntityOut>>> include)
            where TEntityOut : IEntityModel<TIdOut>
            where TIdOut : notnull =>
            await ResultCollectionTryAsync(() => _databaseSet.Include(include).ToListAsync(), TableAccessError);

        /// <summary>
        /// Вернуть запись из таблицы по идентификатору асинхронно
        /// </summary>
        public async Task<IResultValue<TEntity>> FindAsync(TId id) =>
            await ResultValueBindTryAsync(() => _databaseSet.FindAsync(id).MapValueToTask()!.
                                                ToResultValueNullCheckTaskAsync(DatabaseErrors.ValueNotFoundError(id.ToString()!, TableName)),
                                          TableAccessError);

        /// <summary>
        /// Вернуть запись из таблицы по идентификатору асинхронно с включением сущностей
        /// </summary>
        public async Task<IResultValue<TEntity>> FindAsync<TIdOut, TEntityOut>(TId id, Expression<Func<TEntity, IEnumerable<TEntityOut>>> include)
            where TEntityOut : IEntityModel<TIdOut>
            where TIdOut : notnull =>
            await FindAsyncWrapper(_databaseSet.Include(include).FirstOrDefaultAsync(IdPredicate(id)), id);

        /// <summary>
        /// Вернуть запись из таблицы по идентификатору асинхронно с включением сущностей
        /// </summary>
        public async Task<IResultValue<TEntity>> FindAsync<TIdOut, TEntityOut>(TId id, Expression<Func<TEntity, TEntityOut>> include)
            where TEntityOut :  IEntityModel<TIdOut>
            where TIdOut : notnull =>
            await FindAsyncWrapper(_databaseSet.Include(include).FirstOrDefaultAsync(IdPredicate(id)), id);

        /// <summary>
        /// Найти записи в таблице по идентификаторам
        /// </summary>
        public async Task<IResultCollection<TEntity>> FindAsync(IEnumerable<TId> ids) =>
            await FindAsync(_databaseSet.Where(IdsPredicate(ids)).ToListAsync());

        /// <summary>
        /// Найти записи в таблице по идентификаторам с включением сущностей
        /// </summary>
        public async Task<IResultCollection<TEntity>> FindAsync<TIdOut, TEntityOut>(IEnumerable<TId> ids,
                                                                                    Expression<Func<TEntity, IEnumerable<TEntityOut>>> include)
            where TEntityOut : IEntityModel<TIdOut>
            where TIdOut : notnull =>
            await FindAsync(_databaseSet.Where(IdsPredicate(ids)).Include(include).ToListAsync());

        /// <summary>
        /// Добавить список в таблицу
        /// </summary>
        public async Task<IResultCollection<TId>> AddRangeAsync(IEnumerable<TEntity> entities) =>
            await ResultErrorTryAsync(() => _databaseSet.AddRangeAsync(entities), TableAccessError).
            ToResultValueTaskAsync(entities.Select(entity => entity.Id)).
            ToResultCollectionTaskAsync();

        /// <summary>
        /// Обновить элемент в таблице
        /// </summary>
        public new IResultError Update(TEntity entity) =>
            ResultErrorTry(() => _databaseSet.Update(entity), TableAccessError);

        /// <summary>
        /// Удалить элемент в таблице
        /// </summary>
        public new IResultValue<TEntity> Remove(TEntity entity) =>
            ResultValueTry(() => _databaseSet.Remove(entity).Entity, TableAccessError);

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        protected abstract Expression<Func<TEntity, bool>> IdPredicate(TId id);

        /// <summary>
        /// Поиск по параметрам
        /// </summary>
        protected abstract Expression<Func<TEntity, bool>> IdsPredicate(IEnumerable<TId> ids);

        /// <summary>
        /// Ошибка доступа к таблице базы данных
        /// </summary>
        private IErrorResult TableAccessError => DatabaseErrors.TableAccessError(TableName);

        /// <summary>
        /// Поиск элемента с проверкой
        /// </summary>
        private async Task<IResultValue<TEntity>> FindAsyncWrapper(Task<TEntity> entity, TId id) =>
            await ResultValueBindTryAsync(() => entity!.
                                                ToResultValueNullCheckTaskAsync(DatabaseErrors.ValueNotFoundError(id.ToString()!, TableName)),
                                          TableAccessError);

        /// <summary>
        /// Найти записи в таблице по идентификаторам
        /// </summary>
        public async Task<IResultCollection<TEntity>> FindAsync(Task<List<TEntity>> entities) =>
            await ResultCollectionTryAsync(() => entities, TableAccessError);
    }
}