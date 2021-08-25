using System.Collections;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using Functional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Interfaces.Converters.Base
{
    /// <summary>
    /// Базовый конвертер из доменной модели в модель базы данных
    /// </summary>
    public interface IEntityConverter<TId, TDomain, TEntity>
        where TDomain : IDomainModel<TId>
        where TEntity : class, IEntityModel<TId>
        where TId: notnull
    {
        /// <summary>
        /// Преобразовать модель базы данных в доменную
        /// </summary>
        IResultValue<TDomain> FromEntity(TEntity entity);

        /// <summary>
        /// Преобразовать доменную модель в модель базы данных
        /// </summary>
        TEntity ToEntity(TDomain domain);

        /// <summary>
        /// Преобразовать модели базы данных в доменные
        /// </summary>
        IResultCollection<TDomain> FromEntities(IEnumerable<TEntity> entities);

        /// <summary>
        /// Преобразовать доменные модели в модели базы данных
        /// </summary>
        IEnumerable<TEntity> ToEntities(IEnumerable<TDomain> domains);
    }
}