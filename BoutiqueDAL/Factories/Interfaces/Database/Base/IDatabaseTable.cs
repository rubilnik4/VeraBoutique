using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task<IList<TEntity>> ToListAsync();

        /// <summary>
        /// Добавить список в таблицу
        /// </summary>
        Task AddRangeAsync(IEnumerable<TEntity> entities);
    }
}