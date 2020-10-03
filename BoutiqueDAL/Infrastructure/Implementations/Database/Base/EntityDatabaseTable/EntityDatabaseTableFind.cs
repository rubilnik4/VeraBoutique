using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using Functional.Models.Interfaces.Result;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Base.EntityDatabaseTable
{
    /// <summary>
    /// Таблица базы данных EntityFramework. Функции нахождения данных
    /// </summary>
    public abstract partial class EntityDatabaseTable<TId, TEntity>
        where TEntity : class, IEntityModel<TId>
        where TId : notnull
    {
        /// <summary>
        /// Вернуть запись из таблицы по идентификатору асинхронно
        /// </summary>
        public async Task<IResultValue<TEntity>> FindAsync(TId id) =>
            await FindAsyncWrapper(() => _databaseSet.AsNoTracking().FirstOrDefaultAsync(IdPredicate(id))!, id);

        /// <summary>
        /// Вернуть запись из таблицы по идентификатору асинхронно с включением сущностей
        /// </summary>
        public async Task<IResultValue<TEntity>> FindAsync<TIdOut, TEntityOut>(TId id, Expression<Func<TEntity, IEnumerable<TEntityOut>>> include)
            where TEntityOut : IEntityModel<TIdOut>
            where TIdOut : notnull =>
            await FindAsyncWrapper(() => _databaseSet.AsNoTracking().Include(include).FirstOrDefaultAsync(IdPredicate(id))!, id);

        /// <summary>
        /// Вернуть запись из таблицы по идентификатору асинхронно с включением сущностей
        /// </summary>
        public async Task<IResultValue<TEntity>> FindAsync<TIdOut, TEntityOut>(TId id, Expression<Func<TEntity, TEntityOut>> include)
            where TEntityOut : IEntityModel<TIdOut>
            where TIdOut : notnull =>
            await FindAsyncWrapper(() => _databaseSet.AsNoTracking().Include(include).FirstOrDefaultAsync(IdPredicate(id))!, id);

        /// <summary>
        /// Найти записи в таблице по идентификаторам
        /// </summary>
        public async Task<IResultCollection<TEntity>> FindAsync(IEnumerable<TId> ids) =>
            await FindAsync(() => _databaseSet.AsNoTracking().Where(IdsPredicate(ids)).ToListAsync());

        /// <summary>
        /// Найти записи в таблице по идентификаторам с включением сущностей
        /// </summary>
        public async Task<IResultCollection<TEntity>> FindAsync<TIdOut, TEntityOut>(IEnumerable<TId> ids,
                                                                                    Expression<Func<TEntity, IEnumerable<TEntityOut>>> include)
            where TEntityOut : IEntityModel<TIdOut>
            where TIdOut : notnull =>
            await FindAsync(() => _databaseSet.AsNoTracking().Where(IdsPredicate(ids)).Include(include).ToListAsync());
    }
}