using System.Collections;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Base;

namespace BoutiqueDAL.Infrastructure.Interfaces.Converters.Base
{
    /// <summary>
    /// Базовый конвертер из доменной модели в модель базы данных
    /// </summary>
    public interface IEntityConverter<TId, TModel, TEntity>
        where TModel: IModel<TId>
        where TEntity: IEntityModel<TId>
        where TId: notnull
    {
        /// <summary>
        /// Преобразовать модель базы данных в доменную
        /// </summary>
        TModel FromEntity(TEntity entity);

        /// <summary>
        /// Преобразовать доменную модель в модель базы данных
        /// </summary>
        TEntity ToEntity(TModel gender);

        /// <summary>
        /// Преобразовать модели базы данных в доменные
        /// </summary>
        public IEnumerable<TModel> FromEntities(IEnumerable<TEntity> entities) =>
            entities.Select(FromEntity);

        /// <summary>
        /// Преобразовать доменные модели в модели базы данных
        /// </summary>
        public IEnumerable<TEntity> ToEntities(IEnumerable<TModel> entities) =>
            entities.Select(ToEntity);
    }
}