using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;

namespace BoutiqueXamarin.Models.Implementations.Navigation.Clothes
{
    /// <summary>
    /// Параметры перехода к детализации одежды
    /// </summary>
    public class ClothesDetailNavigationParameters : BaseNavigationParameters
    {
        public ClothesDetailNavigationParameters(IClothesDetailDomain clothesDetail, SizeType defaultSizeType)
        {
            ClothesDetail = clothesDetail;
            DefaultSizeType = defaultSizeType;
        }

        /// <summary>
        /// Тип пола
        /// </summary>
        public IClothesDetailDomain ClothesDetail { get; }

        /// <summary>
        /// Размер по умолчанию
        /// </summary>
        public SizeType DefaultSizeType { get; }
    }
}