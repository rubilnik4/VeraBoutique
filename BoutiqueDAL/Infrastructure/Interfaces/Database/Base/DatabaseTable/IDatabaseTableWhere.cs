using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Infrastructure.Interfaces.Database.Base.DatabaseTable
{
    /// <summary>
    /// Таблица базы данных. Функции поиска данных
    /// </summary>
    public interface IDatabaseTableWhere<in TId, TEntity>
        where TEntity : IEntityModel<TId>
        where TId : notnull
    {
        /// <summary>
        /// Запрос поиска по идентификатору
        /// </summary>
        IQueryable<TEntity> Where(TId id);

        /// <summary>
        /// Запрос поиска по идентификаторам
        /// </summary>
        IQueryable<TEntity> Where(IEnumerable<TId> ids);

        /// <summary>
        /// Запрос поиска по идентификатору с включением сущностей
        /// </summary>
        IQueryable<TEntity> Where<TIdOut, TEntityOut>(TId id,
                                                      Expression<Func<TEntity, IEnumerable<TEntityOut>?>> include)
            where TEntityOut : IEntityModel<TIdOut>
            where TIdOut : notnull;

        /// <summary>
        /// Запрос поиска по идентификаторам  с включением сущностей
        /// </summary>
        IQueryable<TEntity> Where<TIdOut, TEntityOut>(IEnumerable<TId> ids,
                                                      Expression<Func<TEntity, IEnumerable<TEntityOut>?>> include)
            where TEntityOut : IEntityModel<TIdOut>
            where TIdOut : notnull;

    }
}