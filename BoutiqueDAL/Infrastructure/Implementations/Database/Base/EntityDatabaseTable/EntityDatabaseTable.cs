using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.Models.Interfaces.Result;
using Microsoft.EntityFrameworkCore;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection.ResultCollectionTryAsyncExtensions;
using static Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue.ResultValueTryExtensions;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultValue.ResultValueBindTryAsyncExtensions;
using static Functional.FunctionalExtensions.Sync.ResultExtension.ResultError.ResultErrorTryExtensions;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultError.ResultErrorTryAsyncExtensions;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Base.EntityDatabaseTable
{
    /// <summary>
    /// Таблица базы данных EntityFramework
    /// </summary>
    public abstract partial class EntityDatabaseTable<TId, TEntity> : DbSet<TEntity>, IDatabaseTable<TId, TEntity>
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
        /// Функция поиска по идентификатору
        /// </summary>
        protected abstract Expression<Func<TEntity, bool>> IdPredicate(TId id);

        /// <summary>
        /// Поиск по параметрам
        /// </summary>
        protected abstract Expression<Func<TEntity, bool>> IdsPredicate(IEnumerable<TId> ids);

        /// <summary>
        /// Поиск элемента с проверкой
        /// </summary>
        private async Task<IResultValue<TEntity>> FindAsyncWrapper(Func<Task<TEntity?>> getEntity, TId id) =>
            await ResultValueBindTryAsync(() => getEntity().
                                                ToResultValueNullCheckTaskAsync(DatabaseErrors.ValueNotFoundError(id.ToString()!, TableName)),
                                          TableAccessError);

        /// <summary>
        /// Найти записи в таблице по идентификаторам
        /// </summary>
        private async Task<IResultCollection<TEntity>> FindAsync(Func<Task<List<TEntity>>> getEntities) =>
            await ResultCollectionTryAsync(getEntities, TableAccessError);

        /// <summary>
        /// Ошибка доступа к таблице базы данных
        /// </summary>
        private IErrorResult TableAccessError => DatabaseErrors.TableAccessError(TableName);
    }
}