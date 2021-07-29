using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Interfaces.Database.Base.DatabaseTable
{
    /// <summary>
    /// Таблица базы данных. Функции нахождения данных
    /// </summary>
    public interface IDatabaseTableFind<in TId, TEntity>
        where TEntity : class, IEntityModel<TId>
        where TId : notnull
    {
        /// <summary>
        /// Вернуть запись из таблицы по идентификатору асинхронно
        /// </summary>
        Task<IResultValue<TEntity>> FindByIdAsync(TId id);

        /// <summary>
        /// Вернуть полную запись из таблицы по идентификатору асинхронно
        /// </summary>
        Task<IResultValue<TEntity>> FindMainByIdAsync(TId id);

        /// <summary>
        /// Найти записи в таблице по идентификаторам
        /// </summary>
        Task<IResultCollection<TEntity>> FindByIdsAsync(IEnumerable<TId> ids);

        /// <summary>
        /// Вернуть полные записи из таблицы по идентификаторам асинхронно
        /// </summary>
        Task<IResultCollection<TEntity>> FindMainByIdsAsync(IEnumerable<TId> ids);

        /// <summary>
        /// Выполнить запрос в таблице и выгрузить сущности
        /// </summary>
        Task<IResultValue<TOut>> FindExpressionValueAsync<TOut>(Func<IQueryable<TEntity>, Task<TOut>> queryFunc, TId id)
             where TOut : notnull;

        /// <summary>
        /// Выполнить запрос в таблице и выгрузить сущности
        /// </summary>
        Task<IResultValue<TEntityOut>> FindExpressionAsync<TEntityOut>(Func<IQueryable<TEntity>, Task<TEntityOut?>> queryFunc, TId id)
            where TEntityOut : class, IEntityModel<TId>;

        /// <summary>
        /// Выполнить запрос в таблице и выгрузить сущности
        /// </summary>
        Task<IResultCollection<TOut>> FindsExpressionValueAsync<TOut>(Func<IQueryable<TEntity>, IQueryable<TOut>> queryFunc)
             where TOut : notnull;

        /// <summary>
        /// Выполнить запрос в таблице и выгрузить сущности
        /// </summary>
        Task<IResultCollection<TEntityOut>> FindsExpressionAsync<TEntityOut>(Func<IQueryable<TEntity>, IQueryable<TEntityOut>> queryFunc)
             where TEntityOut : class, IEntityModel<TId>;
    }
}