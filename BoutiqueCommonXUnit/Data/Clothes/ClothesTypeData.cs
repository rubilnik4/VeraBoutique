﻿using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommonXUnit.Data.Clothes
{
    /// <summary>
    /// Данные вида одежды
    /// </summary>
    public static class ClothesTypeData
    {
        /// <summary>
        /// Получить виды одежды
        /// </summary>
        public static IReadOnlyCollection<IClothesTypeDomain> ClothesTypeDomains =>
            new List<IClothesTypeDomain>()
            {
                new ClothesTypeDomain("Пиджак", CategoryData.CategoryDomain.First(), GenderData.GendersDomain),
                new ClothesTypeDomain("Брюки", CategoryData.CategoryDomain.Last(), GenderData.GendersDomain),
            };

        /// <summary>
        /// Получить основную информацию видов одежды
        /// </summary>
        public static IReadOnlyCollection<IClothesTypeShortDomain> ClothesTypeShortDomains =>
            ClothesTypeDomains;
    }
}