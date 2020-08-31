using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueDAL.Entities.Clothes;
using BoutiqueDAL.Factories.Implementations.Database.Errors;
using BoutiqueDAL.Factories.Interfaces.Database.Base;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Microsoft.EntityFrameworkCore;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection.ResultCollectionTryAsyncExtensions;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultError.ResultErrorTryAsyncExtensions;

namespace BoutiqueDAL.Factories.Implementations.Database.Base
{
    /// <summary>
    /// Таблица базы данных EntityFramework
    /// </summary>
    public class EntityDatabaseTable<TEntity> : DbSet<TEntity>, IDatabaseTable<TEntity>
        where TEntity : class
    {
        public EntityDatabaseTable(DbSet<TEntity> databaseSet, string tableName)
        {
            _databaseSet = databaseSet;
            _tableName = tableName;
        }

        /// <summary>
        /// Экземпляр таблицы базы данных
        /// </summary>
        private readonly DbSet<TEntity> _databaseSet;

        /// <summary>
        /// Имя таблицы
        /// </summary>
        private readonly string _tableName;

        /// <summary>
        /// Вернуть записи из таблицы асинхронно
        /// </summary>
        public async Task<IResultCollection<TEntity>> ToListAsync() =>
            await ResultCollectionTryAsync(() => _databaseSet.ToListAsync(), 
                                           DatabaseErrors.TableAccessError(_tableName));

        /// <summary>
        /// Добавить список в таблицу
        /// </summary>
        public async Task<IResultError> AddRangeAsync(IEnumerable<TEntity> entities) =>
            await ResultErrorTryAsync(() => _databaseSet.AddRangeAsync(entities), 
                                      DatabaseErrors.TableAccessError(_tableName));
    }
}