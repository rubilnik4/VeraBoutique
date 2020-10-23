using System.Collections;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Domain.Interfaces.Base;

namespace BoutiqueDAL.Infrastructure.Interfaces.Converters.Base
{
    /// <summary>
    /// Базовый конвертер из доменной модели в модель базы данных
    /// </summary>
    public interface IEntityConverter<TId, TDomain, in TEntityIn, out TEntityOut>
        where TDomain : IDomainModel<TId>
        where TEntityIn : IEntityModel<TId>
        where TEntityOut : class, TEntityIn
        where TId: notnull
    {
        /// <summary>
        /// Преобразовать модель базы данных в доменную
        /// </summary>
        TDomain FromEntity(TEntityIn entity);

        /// <summary>
        /// Преобразовать доменную модель в модель базы данных
        /// </summary>
        TEntityOut ToEntity(TDomain domain);

        /// <summary>
        /// Преобразовать модели базы данных в доменные
        /// </summary>
        IEnumerable<TDomain> FromEntities(IEnumerable<TEntityIn> entities);

        /// <summary>
        /// Преобразовать доменные модели в модели базы данных
        /// </summary>
        IEnumerable<TEntityOut> ToEntities(IEnumerable<TDomain> domains);
    }
}