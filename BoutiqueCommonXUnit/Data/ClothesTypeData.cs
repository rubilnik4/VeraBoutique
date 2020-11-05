using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommonXUnit.Data
{
    /// <summary>
    /// Данные вида одежды
    /// </summary>
    public static class ClothesTypeData
    {
        /// <summary>
        /// Получить виды одежды
        /// </summary>
        public static List<IClothesTypeFullDomain> GetClothesTypeFullDomain() =>
            new List<IClothesTypeFullDomain>()
            {
                new ClothesTypeFullDomain("Пиджак", new CategoryDomain("Верхняя одежда"),
                                          new List<IGenderDomain> { new GenderDomain(GenderType.Male, "Мужик") }),
                new ClothesTypeFullDomain("Брюки", new CategoryDomain("Штаны"),
                                          new List<IGenderDomain> { new GenderDomain(GenderType.Male, "Мужик") }),
            };

        /// <summary>
        /// Получить основную информацию видов одежды
        /// </summary>
        public static List<IClothesTypeShortDomain> GetClothesTypeShortDomain() =>
            new List<IClothesTypeShortDomain>()
            {
                new ClothesTypeShortDomain("Пиджак", new CategoryDomain("Верхняя одежда"),
                                           new GenderDomain(GenderType.Male, "Мужик")),
                new ClothesTypeShortDomain("Брюки", new CategoryDomain("Штаны"),
                                          new GenderDomain(GenderType.Male, "Мужик")),
            };
    }
}