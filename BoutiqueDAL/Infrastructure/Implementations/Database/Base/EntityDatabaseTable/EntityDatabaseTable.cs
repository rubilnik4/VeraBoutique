﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base.DatabaseTable;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.Models.Interfaces.Result;
using Microsoft.EntityFrameworkCore;


namespace BoutiqueDAL.Infrastructure.Implementations.Database.Base.EntityDatabaseTable
{
    /// <summary>
    /// Таблица базы данных EntityFramework
    /// </summary>
    public abstract partial class EntityDatabaseTable<TId, TDomain, TEntity> : 
        DbSet<TEntity>, IDatabaseTable<TId, TDomain, TEntity>
        where TDomain : IDomainModel<TId>
        where TEntity : class, IEntityModel<TId>
        where TId : notnull
    {
        protected EntityDatabaseTable(DbSet<TEntity> databaseSet)
        {
            _databaseSet = databaseSet;
        }

        /// <summary>
        /// Экземпляр таблицы базы данных
        /// </summary>
        private readonly DbSet<TEntity> _databaseSet;

        /// <summary>
        /// Имя таблицы
        /// </summary>
        public string TableName => GetType().Name;

        /// <summary>
        /// Выгрузка идентификатора
        /// </summary>
        public abstract Expression<Func<TEntity, TId>> IdSelect();

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        public abstract Expression<Func<TEntity, bool>> IdPredicate(TId id);

        /// <summary>
        /// Поиск по параметрам
        /// </summary>
        public abstract Expression<Func<TEntity, bool>> IdsPredicate(IEnumerable<TId> ids);

        /// <summary>
        /// Поиск для проверки сущностей
        /// </summary>
        public virtual Expression<Func<TEntity, bool>> DomainsCheck(IReadOnlyCollection<TDomain> domains) =>
            IdsPredicate(domains.Select(entity => entity.Id));

        /// <summary>
        /// Ошибка доступа к таблице базы данных
        /// </summary>
        private IErrorResult TableAccessError => DatabaseErrors.TableAccessError(TableName);
    }
}