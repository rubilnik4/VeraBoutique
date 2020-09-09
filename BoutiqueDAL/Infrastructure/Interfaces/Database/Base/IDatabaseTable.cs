﻿using System.Collections.Generic;
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
        /// Вернуть записи из таблицы асинхронно
        /// </summary>
        Task<IResultCollection<TEntity>> ToListAsync();

        /// <summary>
        /// Вернуть запись из таблицы по идентификатору асинхронно
        /// </summary>
        Task<IResultValue<TEntity>> FirstAsync(TId id);

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