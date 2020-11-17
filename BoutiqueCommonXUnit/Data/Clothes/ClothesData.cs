using System.Collections.Generic;
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
        /// Получить одежду
        /// </summary>
        public static List<IClothesShortDomain> ClothesShortDomains =>
            new List<IClothesShortDomain>
            {
                new ClothesShortDomain(1, "Верхонки", 55.55m, null),
                new ClothesShortDomain(2,"Варежки", 0.66m, null),
            };

        /// <summary>
        /// Получить информацию об одежде
        /// </summary>
        public static List<IClothesDomain> ClothesDomains =>
            new List<IClothesDomain>
            {
                new ClothesDomain(1, "Верхонки", 55.55m, null,"Верхонки батраческие",
                                             new GenderDomain(GenderType.Male, "Мужик"), 
                                             new ClothesTypeShortDomain("Перчатки", new CategoryDomain("Напалечники")),
                                             ColorClothesData.ColorClothesDomain,
                                             SizeGroupData.SizeGroupDomain),
                new ClothesDomain(2, "Варежки", 0.66m, null, "Варежки простолюдинные",
                                             new GenderDomain(GenderType.Female, "Женщина"),
                                             new ClothesTypeShortDomain("Перчатки", new CategoryDomain("Напалечники")),
                                             ColorClothesData.ColorClothesDomain,
                                             SizeGroupData.SizeGroupDomain),
            };
    }
}