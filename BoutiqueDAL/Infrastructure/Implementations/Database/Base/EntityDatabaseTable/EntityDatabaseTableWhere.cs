using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Base.EntityDatabaseTable
{
    /// <summary>
    /// Таблица базы данных EntityFramework. Функции поиска данных
    /// </summary>
    public abstract partial class EntityDatabaseTable<TId, TDomain, TEntity>
        where TDomain : IDomainModel<TId>
        where TEntity : class, IEntityModel<TId>
        where TId : notnull
    {
        /// <summary>
        /// Запрос поиска по идентификатору
        /// </summary>
        public IQueryable<TEntity> Where(TId id) =>
            _databaseSet.Where(IdPredicate(id));

        /// <summary>
        /// Запрос поиска по идентификаторам
        /// </summary>
        public IQueryable<TEntity> Where(IEnumerable<TId> ids) =>
            _databaseSet.Where(IdsPredicate(ids));

        /// <summary>
        /// Запрос поиска по идентификатору с включением сущностей
        /// </summary>
        public IQueryable<TEntity> Where<TIdOut, TEntityOut>(TId id,
                                                             Expression<Func<TEntity, IEnumerable<TEntityOut>?>> include)
            where TEntityOut : IEntityModel<TIdOut>
            where TIdOut : notnull =>
            _databaseSet.Where(IdPredicate(id)).Include(include);

        /// <summary>
        /// Запрос поиска по идентификаторам  с включением сущностей
        /// </summary>
        public IQueryable<TEntity> Where<TIdOut, TEntityOut>(IEnumerable<TId> ids,
                                                             Expression<Func<TEntity, IEnumerable<TEntityOut>?>> include)
            where TEntityOut : IEntityModel<TIdOut>
            where TIdOut : notnull =>
            _databaseSet.Where(IdsPredicate(ids)).Include(include);
    }
}