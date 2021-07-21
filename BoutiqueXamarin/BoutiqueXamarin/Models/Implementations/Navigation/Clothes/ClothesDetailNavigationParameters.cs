using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;

namespace BoutiqueXamarin.Models.Implementations.Navigation.Clothes
{
    /// <summary>
    /// Параметры перехода к детализации одежды
    /// </summary>
    public class ClothesDetailNavigationParameters : EmptyNavigationParameters
    {
        public ClothesDetailNavigationParameters(IClothesDetailDomain clothesDetail)
        {
            ClothesDetail = clothesDetail;
        }

        /// <summary>
        /// Тип пола
        /// </summary>
        public IClothesDetailDomain ClothesDetail { get; }
    }
}