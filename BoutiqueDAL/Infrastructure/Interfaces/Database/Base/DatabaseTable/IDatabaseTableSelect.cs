using System;
using System.Linq;
using System.Linq.Expressions;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Infrastructure.Interfaces.Database.Base.DatabaseTable
{
    /// <summary>
    /// Таблица базы данных. Функции выбора данных
    /// </summary>
    public interface IDatabaseTableSelect<in TId, TEntity>
        where TEntity : IEntityModel<TId>
        where TId : notnull
    {
        /// <summary>
        /// Запрос выбора
        /// </summary>
        IQueryable<TEntitySelect> Select<TEntitySelect>(Expression<Func<TEntity, TEntitySelect>> selectFunc)
            where TEntitySelect : class, IEntityModel<TId>;
    }
}