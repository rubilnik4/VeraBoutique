using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;

namespace BoutiqueXamarin.Models.Implementations.Navigation.Clothes
{
    /// <summary>
    /// Параметры перехода к списку одежды
    /// </summary>
    public class ClothesNavigationParameters : BaseNavigationParameters
    {
        public ClothesNavigationParameters(GenderType genderType, IClothesTypeDomain clothesTypeDomain)
        {
            GenderType = genderType;
            ClothesTypeDomain = clothesTypeDomain;
        }

        /// <summary>
        /// Тип пола
        /// </summary>
        public GenderType GenderType { get; }

        /// <summary>
        /// Тип пола
        /// </summary>
        public IClothesTypeDomain ClothesTypeDomain { get; }
    }
}