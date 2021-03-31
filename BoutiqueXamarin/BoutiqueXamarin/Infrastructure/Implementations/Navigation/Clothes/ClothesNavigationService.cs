using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueXamarin.Infrastructure.Implementations.Navigation.Base;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.Views.Clothes.Clothes;
using Prism.Navigation;

namespace BoutiqueXamarin.Infrastructure.Implementations.Navigation.Clothes
{
    /// <summary>
    /// Сервис навигации к странице одежды
    /// </summary>
    public class ClothesNavigationService : BaseNavigationService<ClothesNavigationParameters, ClothesPage>,
                                            IClothesNavigationService
    {
        public ClothesNavigationService(INavigationService navigationService)
            : base(navigationService)
        { }

        /// <summary>
        /// Перейти к странице
        /// </summary>
        public async Task NavigateTo(GenderType genderType, string clothesType) =>
            await NavigateTo(new ClothesNavigationParameters(genderType, clothesType));
    }
}