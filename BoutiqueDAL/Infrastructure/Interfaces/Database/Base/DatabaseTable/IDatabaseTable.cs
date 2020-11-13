using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Interfaces.Database.Base.DatabaseTable
{
    /// <summary>
    /// Таблица базы данных
    /// </summary>
    public interface IDatabaseTable<TId, in TDomain, TEntity>: 
        IDatabaseTableCrud<TId, TEntity>, IDatabaseTableFind<TId, TEntity>,
        IDatabaseTableSelect<TId, TEntity>, IDatabaseTableToList<TId, TEntity>,
        IDatabaseTableWhere<TId, TEntity> 
        where TDomain: IDomainModel<TId>
        where TEntity : IEntityModel<TId>
        where TId: notnull
    {
        /// <summary>
        /// Имя таблицы
        /// </summary>
        string TableName { get; }

        /// <summary>
        /// Выгрузка идентификатора
        /// </summary>
        Expression<Func<TEntity, TId>> IdSelect();

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        Expression<Func<TEntity, bool>> IdPredicate(TId id);

        /// <summary>
        /// Поиск по параметрам
        /// </summary>
        Expression<Func<TEntity, bool>> IdsPredicate(IEnumerable<TId> ids);
        /// <summary>
        /// Функция выбора сущностей для проверки наличия
        /// </summary>
        IQueryable<TEntity> ValidateFilter(IQueryable<TEntity> entities, TDomain domain);

        /// <summary>
        /// Функция выбора сущностей для проверки наличия
        /// </summary>
        IQueryable<TEntity> ValidateFilter(IQueryable<TEntity> entities, IReadOnlyCollection<TDomain> domains);
    }
}