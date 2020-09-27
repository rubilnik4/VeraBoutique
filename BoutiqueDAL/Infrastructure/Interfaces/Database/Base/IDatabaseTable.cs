using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Interfaces.Database.Base
{
    /// <summary>
    /// Таблица базы данных
    /// </summary>
    public interface IDatabaseTable<TId, TEntity>
        where TEntity : IEntityModel<TId>
        where TId: notnull
    {
        /// <summary>
        /// Имя таблицы
        /// </summary>
        string TableName { get; }

        /// <summary>
        /// Вернуть записи из таблицы асинхронно
        /// </summary>
        Task<IResultCollection<TEntity>> ToListAsync();

        /// <summary>
        /// Вернуть запись из таблицы по идентификатору асинхронно
        /// </summary>
        Task<IResultValue<TEntity>> FindAsync(TId id);

        /// <summary>
        /// Найти записи в таблице по идентификаторам
        /// </summary>
        Task<IResultCollection<TEntity>> FindAsync(IEnumerable<TId> ids);

        /// <summary>
        /// Найти записи в таблице по идентификаторам с включением сущностей
        /// </summary>
        Task<IResultCollection<TEntity>> FindAsync<TIdOut, TEntityOut>(IEnumerable<TId> ids, 
                                                                       Func<TEntity, IReadOnlyCollection<TEntityOut>> include)
            where TEntityOut : IEntityModel<TIdOut>
            where TIdOut : notnull;

        /// <summary>
        /// Добавить список в таблицу
        /// </summary>
        Task<IResultCollection<TId>> AddRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Добавить элемент в таблице
        /// </summary>
        IResultError Update(TEntity entity);

        /// <summary>
        /// Удалить элемент в таблице
        /// </summary>
        IResultValue<TEntity> Remove(TEntity entity);
    }
}