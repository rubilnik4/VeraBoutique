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
    public interface IDatabaseTable<TIdType, TEntity>
        where TEntity : BaseEntity<TIdType>
        where TIdType : IEquatable<TIdType>
    {
        /// <summary>
        /// Вернуть записи из таблицы асинхронно
        /// </summary>
        Task<IResultCollection<TEntity>> ToListAsync();

        /// <summary>
        /// Добавить список в таблицу
        /// </summary>
        Task<IResultCollection<TIdType>> AddRangeAsync(IEnumerable<TEntity> entities);
    }
}