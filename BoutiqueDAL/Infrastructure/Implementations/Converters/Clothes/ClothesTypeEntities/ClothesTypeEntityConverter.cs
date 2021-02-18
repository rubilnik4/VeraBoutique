using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Infrastructure.Implementation.Errors;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesTypeEntities
{
    /// <summary>
    /// Преобразования модели основных данных вида одежды в модель базы данных
    /// </summary>
    public class ClothesTypeEntityConverter : EntityConverter<string, IClothesTypeDomain, ClothesTypeEntity>,
                                              IClothesTypeEntityConverter
    {
        /// <summary>
        /// Преобразовать вид одежды из модели базы данных
        /// </summary>
        public override IResultValue<IClothesTypeDomain> FromEntity(ClothesTypeEntity clothesTypeEntity) =>
            new ClothesTypeDomain(clothesTypeEntity).
            Map(clothesTypeShort => new ResultValue<IClothesTypeDomain>(clothesTypeShort));

        /// <summary>
        /// Преобразовать группу размеров одежды в модель базы данных
        /// </summary>
        public override ClothesTypeEntity ToEntity(IClothesTypeDomain clothesTypeDomain) =>
            new ClothesTypeEntity(clothesTypeDomain);
    }
}