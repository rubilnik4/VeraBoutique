using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using Functional.FunctionalExtensions.Sync;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Base.EntityDatabaseTable
{
    /// <summary>
    /// Таблица базы данных EntityFramework. Функции проверки данных
    /// </summary>
    public abstract partial class EntityDatabaseTable<TId, TDomain, TEntity>
        where TDomain : IDomainModel<TId>
        where TEntity : class, IEntityModel<TId>
        where TId : notnull
    {
        /// <summary>
        /// Функция выбора сущностей для проверки наличия
        /// </summary>
        public IQueryable<TEntity> ValidateFilter(IQueryable<TEntity> entities, TDomain domain) =>
           entities.Where(ValidateQuery(entities, domain)).
           Map(entitiesQuery => ValidateInclude(entitiesQuery, new List<TDomain> { domain }));

        /// <summary>
        /// Функция выбора сущностей для проверки наличия
        /// </summary>
        public IQueryable<TEntity> ValidateFilter(IQueryable<TEntity> entities, IReadOnlyCollection<TDomain> domains) =>
           entities.Where(ValidateQuery(entities, domains)).
           Map(entitiesQuery => ValidateInclude(entitiesQuery, domains));

        /// <summary>
        /// Функция для проверки наличия
        /// </summary>
        protected virtual Expression<Func<TEntity, bool>> ValidateQuery(IQueryable<TEntity> entities, TDomain domain) =>
            IdPredicate(domain.Id);

        /// <summary>
        /// Функция для проверки наличия
        /// </summary>
        protected virtual Expression<Func<TEntity, bool>> ValidateQuery(IQueryable<TEntity> entities,
                                                                        IReadOnlyCollection<TDomain> domains) =>
           IdsPredicate(domains.Select(domain => domain.Id));

        /// <summary>
        /// Функция проверки наличия вложенных сущностей
        /// </summary>
        protected virtual IQueryable<TEntity> ValidateInclude(IQueryable<TEntity> entities, IReadOnlyCollection<TDomain> domains) =>
            entities;
    }
}