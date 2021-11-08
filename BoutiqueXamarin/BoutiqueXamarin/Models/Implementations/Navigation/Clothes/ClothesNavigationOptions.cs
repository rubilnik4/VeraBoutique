using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;

namespace BoutiqueXamarin.Models.Implementations.Navigation.Clothes
{
    /// <summary>
    /// Параметры перехода к списку одежды
    /// </summary>
    public class ClothesNavigationOptions : BaseNavigationOptions
    {
        public ClothesNavigationOptions(IReadOnlyCollection<IClothesDetailDomain> clothesDetails, SizeType sizeTypeDefault)
        {
            ClothesDetails = clothesDetails;
            SizeTypeDefault = sizeTypeDefault;
        }

        /// <summary>
        /// Информация об одежде
        /// </summary>
        public IReadOnlyCollection<IClothesDetailDomain> ClothesDetails { get; }

        /// <summary>
        /// Тип размера по умолчанию
        /// </summary>
        public SizeType SizeTypeDefault { get; }
    }
}