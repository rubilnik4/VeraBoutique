﻿using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesEntities
{
    /// <summary>
    /// Преобразования модели одежды в модель базы данных
    /// </summary>
    public class ClothesEntityConverter : EntityConverter<int, IClothesDomain, ClothesEntity>,
                                          IClothesEntityConverter
    {
        /// <summary>
        /// Преобразовать категорию одежды из модели базы данных
        /// </summary>
        public override IResultValue<IClothesDomain> FromEntity(ClothesEntity clothesEntity) =>
            new ClothesDomain(clothesEntity).
            ToResultValue();

        /// <summary>
        /// Преобразовать категорию одежды в модель базы данных
        /// </summary>
        public override ClothesEntity ToEntity(IClothesDomain clothesDomain) =>
            new (clothesDomain);
    }
}