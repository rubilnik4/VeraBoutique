using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
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
        public static List<IClothesInformationDomain> ClothesInformationDomains =>
            new List<IClothesInformationDomain>
            {
                new ClothesInformationDomain(1, "Верхонки", 55.55m, null,"Верхонки батраческие",
                                             new GenderDomain(GenderType.Male, "Мужик"), 
                                             new ClothesTypeDomain("Перчатки", new CategoryDomain("Перчатки")),
                                             ColorClothesData.GetColorClothesDomain(),
                                             SizeGroupData.GetSizeGroupDomain()),
                new ClothesInformationDomain(2, "Варежки", 0.66m, null, "Варежки простолюдинные",
                                             new GenderDomain(GenderType.Female, "Женщина"),
                                             new ClothesTypeDomain("Перчатки", new CategoryDomain("Перчатки")),
                                             ColorClothesData.GetColorClothesDomain(),
                                             SizeGroupData.GetSizeGroupDomain()),
            };
    }
}