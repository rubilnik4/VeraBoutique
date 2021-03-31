using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;

namespace BoutiqueXamarin.Models.Implementations.Navigation.Clothes
{
    public class ClothesNavigationParameters : BaseNavigationParameters
    {
        public ClothesNavigationParameters(GenderType genderType, string clothesType)
        {
            GenderType = genderType;
            ClothesType = clothesType;
        }

        /// <summary>
        /// Тип пола
        /// </summary>
        public GenderType GenderType { get; }

        /// <summary>
        /// Тип одежды
        /// </summary>
        public string ClothesType { get; }
    }
}