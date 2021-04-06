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
        where TEntity : IEntityModel<TId>
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
        Task <IResultValue<TOut>> FindExpressionAsync<TOut>(Func<IQueryable<TEntity>, IQueryable<TOut>> queryFunc, TId id)
            where TOut : notnull;

        /// <summary>
        /// Выполнить запрос в таблице и выгрузить сущности
        /// </summary>
        Task<IResultCollection<TOut>> FindsExpressionAsync<TOut>(Func<IQueryable<TEntity>, IQueryable<TOut>> queryFunc)
            where TOut : notnull;
    }
}