using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueDAL.Entities.Clothes;
using BoutiqueDAL.Factories.Interfaces.Database.Base;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Factories.Implementations.Database.Base
{
    /// <summary>
    /// Таблица базы данных EntityFramework
    /// </summary>
    public class EntityDatabaseTable<TEntity> : DbSet<TEntity>, IDatabaseTable<TEntity>
        where TEntity : class
    {
        public EntityDatabaseTable(DbSet<TEntity> databaseSet)
        {
            _databaseSet = databaseSet;
        }

        /// <summary>
        /// Экземпляр таблицы базы данных
        /// </summary>
        private readonly DbSet<TEntity> _databaseSet;

        /// <summary>
        /// Вернуть записи из таблицы асинхронно
        /// </summary>
        public async Task<IList<TEntity>> ToListAsync() => await _databaseSet.ToListAsync();

        /// <summary>
        /// Добавить список в таблицу
        /// </summary>
        public async Task AddRangeAsync(IEnumerable<TEntity> entities) => await _databaseSet.AddRangeAsync(entities);
    }
}