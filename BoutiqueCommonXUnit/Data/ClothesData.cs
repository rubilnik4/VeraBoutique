using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomain;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomain;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommonXUnit.Data
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
        public static List<IClothesFullDomain> ClothesInformationDomains =>
            new List<IClothesFullDomain>
            {
                new ClothesFullDomain(1, "Верхонки", 55.55m, null,"Верхонки батраческие",
                                             new GenderDomain(GenderType.Male, "Мужик"), 
                                             new ClothesTypeShortDomain("Перчатки", new CategoryDomain("Напалечники")),
                                             ColorClothesData.GetColorClothesDomain(),
                                             SizeGroupData.GetSizeGroupDomain()),
                new ClothesFullDomain(2, "Варежки", 0.66m, null, "Варежки простолюдинные",
                                             new GenderDomain(GenderType.Female, "Женщина"),
                                             new ClothesTypeShortDomain("Перчатки", new CategoryDomain("Напалечники")),
                                             ColorClothesData.GetColorClothesDomain(),
                                             SizeGroupData.GetSizeGroupDomain()),
            };
    }
}