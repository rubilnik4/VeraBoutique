﻿using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesEntities
{
    /// <summary>
    /// Преобразования модели одежды в модель базы данных
    /// </summary>
    public class ClothesShortEntityConverter : EntityConverter<int, IClothesDomain, ClothesEntity>,
                                               IClothesShortEntityConverter
    {
        /// <summary>
        /// Преобразовать категорию одежды из модели базы данных
        /// </summary>
        public override IResultValue<IClothesDomain> FromEntity(ClothesEntity clothesEntity) =>
            new ClothesDomain(clothesEntity).
            Map(clothesShort => new ResultValue<IClothesDomain>(clothesShort));

        /// <summary>
        /// Преобразовать категорию одежды в модель базы данных
        /// </summary>
        public override ClothesEntity ToEntity(IClothesDomain clothesDomain) =>
            new ClothesEntity(clothesDomain);
    }
}