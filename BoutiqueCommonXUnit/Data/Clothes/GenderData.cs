﻿using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.GenderDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommonXUnit.Data.Clothes
{
    /// <summary>
    /// Данные типа пола
    /// </summary>
    public static class GenderData
    {
        /// <summary>
        /// Получить типы пола
        /// </summary>
        public static IReadOnlyCollection<IGenderDomain> GenderDomains =>
            new List<IGenderDomain>()
            {
                new GenderDomain(GenderType.Male, "Мужик" ),
                new GenderDomain(GenderType.Female, "Тетя"),
            };

        /// <summary>
        /// Получить типы пола с категорией
        /// </summary>
        public static IReadOnlyCollection<IGenderCategoryDomain> GenderCategoryDomains =>
            new List<IGenderCategoryDomain>()
            {
                new GenderCategoryDomain(GenderType.Male, "Мужик" , CategoryData.CategoryClothesTypeDomains),
                new GenderCategoryDomain(GenderType.Female, "Тетя", CategoryData.CategoryClothesTypeDomains),
            };
    }
}