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
                new ClothesDomain(1, "Верхонки", "Верхонки батраческие", 55.55m, Properties.Resources.TestImage,
                                  GenderData.GenderDomains.First(),
                                  ClothesTypeData.ClothesTypeShortDomains.First(),
                                  ColorData.ColorDomains,
                                  SizeGroupData.SizeGroupDomains),
                new ClothesDomain(2, "Варежки", "Варежки простолюдинные", 0.66m, Properties.Resources.TestImage,
                                  GenderData.GenderDomains.Last(),
                                  ClothesTypeData.ClothesTypeShortDomains.Last(),
                                  ColorData.ColorDomains,
                                  SizeGroupData.SizeGroupDomains),
            };

        /// <summary>
        /// Получить информацию об одежде
        /// </summary>
        public static IReadOnlyCollection<IClothesShortDomain> ClothesShortDomains =>
            ClothesDomains;
    }
}