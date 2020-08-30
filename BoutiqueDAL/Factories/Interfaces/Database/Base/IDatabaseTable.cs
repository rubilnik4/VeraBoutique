using System.Collections.Generic;
using System.Threading.Tasks;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Factories.Interfaces.Database.Base
{
    /// <summary>
    /// Таблица базы данных
    /// </summary>
    public interface IDatabaseTable<TEntity> where TEntity: class
    {
        /// <summary>
        /// Вернуть записи из таблицы асинхронно
        /// </summary>
        Task<IResultCollection<TEntity>> ToListAsync();

        /// <summary>
        /// Добавить список в таблицу
        /// </summary>
        Task<IResultError> AddRangeAsync(IEnumerable<TEntity> entities);
    }
}