using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.Images;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Images;

namespace BoutiqueCommonXUnit.Data.Clothes
{
    /// <summary>
    /// Изображения
    /// </summary>
    public static class ImageData
    {
        /// <summary>
        /// Получить информацию об одежде
        /// </summary>
        public static IReadOnlyCollection<IClothesImageDomain> ClothesImageDomains =>
            new List<IClothesImageDomain>
            {
                new ClothesImageDomain(1, Properties.Resources.TestImage, true),
                new ClothesImageDomain(2, Properties.Resources.TestImage, false),
            };
    }
}