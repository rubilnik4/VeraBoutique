using System;
using System.Linq;
using System.Linq.Expressions;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Base.EntityDatabaseTable
{
    /// <summary>
    /// Таблица базы данных EntityFramework. Функции выбора данных
    /// </summary>
    public abstract partial class EntityDatabaseTable<TId, TDomain, TEntity>
        where TDomain : IDomainModel<TId>
        where TEntity : class, IEntityModel<TId>
        where TId : notnull
    {
        /// <summary>
        /// Запрос выбора
        /// </summary>
        public IQueryable<TEntitySelect> Select<TEntitySelect>(Expression<Func<TEntity, TEntitySelect>> selectFunc)
            where TEntitySelect: class, IEntityModel<TId> =>
            _databaseSet.Select(selectFunc);
    }
}