﻿using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesTypeEntities;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntity
{
    /// <summary>
    /// Преобразования модели вида одежды в модель базы данных
    /// </summary>
    public interface IClothesTypeEntityConverter : IEntityConverter<string, IClothesTypeFullDomain, IClothesTypeEntity, Models.Implementations.Entities.Clothes.ClothesTypeEntities.ClothesTypeEntity>
    {
        /// <summary>
        /// Преобразовать в базовую модель вида одежды из модели базы данных
        /// </summary>
        public IResultValue<IClothesTypeShortDomain> FromEntityShort(IClothesTypeEntity clothesTypeEntity);

        /// <summary>
        /// Преобразовать в базовые модели вида одежды из моделей базы данных
        /// </summary>
        public IResultCollection<IClothesTypeShortDomain> FromEntityShorts(IEnumerable<IClothesTypeEntity> clothesTypeFullEntities);
    }
}