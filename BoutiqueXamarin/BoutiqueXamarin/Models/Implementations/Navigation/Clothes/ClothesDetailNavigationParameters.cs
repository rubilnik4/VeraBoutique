using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;

namespace BoutiqueXamarin.Models.Implementations.Navigation.Clothes
{
    /// <summary>
    /// Параметры перехода к детализации одежды
    /// </summary>
    public class ClothesDetailNavigationParameters : EmptyNavigationParameters
    {
        public ClothesDetailNavigationParameters(int clothesId)
        {
            ClothesId = clothesId;
        }

        /// <summary>
        /// Тип пола
        /// </summary>
        public int ClothesId { get; }
    }
}