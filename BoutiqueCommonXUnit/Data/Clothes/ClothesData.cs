using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommonXUnit.Data.Clothes
{
    /// <summary>
    /// Данные одежды
    /// </summary>
    public static class ClothesData
    {
        /// <summary>
        /// Получить информацию об одежде
        /// </summary>
        public static IReadOnlyCollection<IClothesDomain> ClothesDomains =>
            new List<IClothesDomain>
            {
                new ClothesDomain(1, "Верхонки", "Верхонки батраческие", 55.55m, null,
                                  new GenderDomain(GenderType.Male, "Мужик"), 
                                  new ClothesTypeShortDomain("Перчатки", "Нарукавники"),
                                  ColorClothesData.ColorClothesDomain,
                                  SizeGroupData.SizeGroupDomain),
                new ClothesDomain(2, "Варежки", "Варежки простолюдинные", 0.66m, null,
                                  new GenderDomain(GenderType.Female, "Женщина"), 
                                  new ClothesTypeShortDomain("Перчатки", "Нарукавники"),
                                  ColorClothesData.ColorClothesDomain,
                                  SizeGroupData.SizeGroupDomain),
            };

        /// <summary>
        /// Получить информацию об одежде
        /// </summary>
        public static List<IClothesShortDomain> ClothesShortDomains =>
            ClothesDomains.Select(clothes => (IClothesShortDomain)clothes).ToList();
    }
}