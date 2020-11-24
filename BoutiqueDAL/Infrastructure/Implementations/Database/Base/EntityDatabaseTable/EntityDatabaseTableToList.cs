using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using Functional.Models.Interfaces.Result;
using Microsoft.EntityFrameworkCore;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection.ResultCollectionTryAsyncExtensions;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Base.EntityDatabaseTable
{
    /// <summary>
    /// Таблица базы данных EntityFramework. Функции выгрузки данных
    /// </summary>
    public abstract partial class EntityDatabaseTable<TId, TDomain, TEntity>
        where TDomain : IDomainModel<TId>
        where TEntity : class, IEntityModel<TId>
        where TId : notnull
    {
        /// <summary>
        /// Вернуть записи из таблицы асинхронно
        /// </summary>
        public async Task<IResultCollection<TEntity>> ToListAsync() =>
            await ResultCollectionTryAsync(() => _databaseSet.AsNoTracking().ToListAsync(), TableAccessError);

        /// <summary>
        /// Вернуть записи из таблицы асинхронно с включением сущностей
        /// </summary>
        public async Task<IResultCollection<TEntity>> ToListAsync<TIdOut, TEntityOut>(Expression<Func<TEntity, IEnumerable<TEntityOut>?>> include)
            where TEntityOut : IEntityModel<TIdOut>
            where TIdOut : notnull =>
            await ResultCollectionTryAsync(() => _databaseSet.AsNoTracking().Include(include).ToListAsync(), TableAccessError);

        /// <summary>
        /// Вернуть полные записи из таблицы асинхронно
        /// </summary>
        public async Task<IResultCollection<TEntity>> ToListMainAsync() =>
            await ResultCollectionTryAsync(() => GetInclude(_databaseSet.AsNoTracking()).
                                                 ToListAsync(), 
                                           TableAccessError);
    }
}