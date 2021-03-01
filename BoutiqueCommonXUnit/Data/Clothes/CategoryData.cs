using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;

namespace BoutiqueCommonXUnit.Data.Clothes
{
    /// <summary>
    /// Данные категорий одежды
    /// </summary>
    public static class CategoryData
    {
        /// <summary>
        /// Получить категории одежды
        /// </summary>
        public static IReadOnlyCollection<ICategoryMainDomain> CategoryMainDomains =>
            new List<ICategoryMainDomain>
            {
                new CategoryMainDomain("Верхняя одежда", GenderData.GenderDomains),
                new CategoryMainDomain("Побрякушки", GenderData.GenderDomains),
            };

        /// <summary>
        /// Получить категории одежды c типом одежды
        /// </summary>
        public static IReadOnlyCollection<ICategoryClothesTypeDomain> CategoryClothesTypeDomains =>
            new List<ICategoryClothesTypeDomain>
            {
                new CategoryClothesTypeDomain("Верхняя одежда", ClothesTypeData.ClothesTypeDomains),
                new CategoryClothesTypeDomain("Побрякушки", ClothesTypeData.ClothesTypeDomains),
            };

        /// <summary>
        /// Получить категории одежды
        /// </summary>
        public static IReadOnlyCollection<ICategoryDomain> CategoryDomains =>
            CategoryMainDomains;
    }
}