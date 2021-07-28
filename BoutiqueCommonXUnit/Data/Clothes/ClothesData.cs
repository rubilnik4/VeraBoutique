using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
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
        public static IReadOnlyCollection<IClothesMainDomain> ClothesMainDomains =>
            new List<IClothesMainDomain>
            {
                new ClothesMainDomain(1, "Верхонки", "Верхонки батраческие", 55.55m,
                                      ImageData.ClothesImageDomains,
                                      GenderData.GenderDomains.First(),
                                      ClothesTypeData.ClothesTypeDomains.First(),
                                      ColorData.ColorDomains,
                                      SizeGroupData.SizeGroupMainDomains),
                new ClothesMainDomain(2, "Варежки", "Варежки простолюдинные", 0.66m,
                                      ImageData.ClothesImageDomains,
                                      GenderData.GenderDomains.Last(),
                                      ClothesTypeData.ClothesTypeDomains.Last(),
                                      ColorData.ColorDomains,
                                      SizeGroupData.SizeGroupMainDomains),
            };

        /// <summary>
        /// Получить информацию об одежде
        /// </summary>
        public static IReadOnlyCollection<IClothesDomain> ClothesDomains =>
            ClothesMainDomains;

        /// <summary>
        /// Получить уточненную информацию об одежде
        /// </summary>
        public static IReadOnlyCollection<IClothesDetailDomain> ClothesDetailDomains =>
            ClothesMainDomains;
    }
}