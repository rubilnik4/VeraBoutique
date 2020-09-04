using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueDAL.Entities.Base;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Factories.Interfaces.Database.Base
{
    /// <summary>
    /// Таблица базы данных
    /// </summary>
    public interface IDatabaseTable<TId, TEntity>
        where TEntity : BaseEntity<TId>
    {
        /// <summary>
        /// Вернуть записи из таблицы асинхронно
        /// </summary>
        Task<IResultCollection<TEntity>> ToListAsync();

        /// <summary>
        /// Добавить список в таблицу
        /// </summary>
        Task<IResultCollection<TId>> AddRangeAsync(IEnumerable<TEntity> entities);
    }
}