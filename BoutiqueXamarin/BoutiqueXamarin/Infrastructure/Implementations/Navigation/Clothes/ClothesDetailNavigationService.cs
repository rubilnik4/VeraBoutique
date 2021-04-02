using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueXamarin.Infrastructure.Implementations.Navigation.Base;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.Views.Clothes.Clothes;
using Prism.Navigation;

namespace BoutiqueXamarin.Infrastructure.Implementations.Navigation.Clothes
{
    public class ClothesDetailNavigationService : BaseNavigationService<ClothesDetailNavigationParameters, ClothesDetailPage>,
                                                  IClothesDetailNavigationService
    {
        public ClothesDetailNavigationService(INavigationService navigationService)
            : base(navigationService)
        { }

        /// <summary>
        /// Перейти к странице
        /// </summary>
        public async Task NavigateTo(int clothesId) =>
            await NavigateTo(new ClothesDetailNavigationParameters(clothesId));
    }
}