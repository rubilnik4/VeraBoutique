using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;

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
        public static List<IClothesInformationDomain> ClothesInformationDomains =>
            new List<IClothesInformationDomain>
            {
                new ClothesInformationDomain(1, "Верхонки", "Верхонки батраческие",
                                             ColorClothesData.GetColorClothesDomain(),
                                             SizeGroupData.GetSizeGroupDomain(), 55.55m, null),
                new ClothesInformationDomain(2,"Варежки", "Варежки простолюдинные", 
                                             ColorClothesData.GetColorClothesDomain(),
                                             SizeGroupData.GetSizeGroupDomain(), 0.66m, null),
            };
    }
}