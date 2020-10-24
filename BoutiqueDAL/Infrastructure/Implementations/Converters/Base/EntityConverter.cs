using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Base
{
    /// <summary>
    /// Базовый конвертер из доменной модели в модель базы данных
    /// </summary>
    public abstract class EntityConverter<TId, TDomain, TEntityIn, TEntityOut> :
        IEntityConverter<TId, TDomain, TEntityIn, TEntityOut>
        where TDomain : IDomainModel<TId>
        where TEntityIn : IEntityModel<TId>
        where TEntityOut : class, TEntityIn
        where TId : notnull
    {
        /// <summary>
        /// Преобразовать модель базы данных в доменную
        /// </summary>
        public abstract IResultValue<TDomain> FromEntity(TEntityIn entity);

        /// <summary>
        /// Преобразовать доменную модель в модель базы данных
        /// </summary>
        public abstract TEntityOut ToEntity(TDomain domain);

        /// <summary>
        /// Преобразовать модели базы данных в доменные
        /// </summary>
        public IResultCollection<TDomain> FromEntities(IEnumerable<TEntityIn> entities) =>
            entities.Select(FromEntity).ToResultCollection();

        /// <summary>
        /// Преобразовать доменные модели в модели базы данных
        /// </summary>
        public IEnumerable<TEntityOut> ToEntities(IEnumerable<TDomain> domains) =>
            domains.Select(ToEntity);
    }
}