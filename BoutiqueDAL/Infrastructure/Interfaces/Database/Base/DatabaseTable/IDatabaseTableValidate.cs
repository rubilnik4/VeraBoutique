using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Infrastructure.Interfaces.Database.Base.DatabaseTable
{
    /// <summary>
    /// Таблица базы данных. Проверка данных
    /// </summary>
    public interface IDatabaseTableValidate<in TId, in TDomain, TEntity>
        where TDomain : IDomainModel<TId>
        where TEntity : IEntityModel<TId>
        where TId : notnull
    {
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