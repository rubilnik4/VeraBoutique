using System.Threading.Tasks;
using BoutiqueXamarin.Views.Clothes.Clothes;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Sync;
using Prism.Navigation;

namespace BoutiqueXamarin.ViewModels.Clothes.Navigation
{
    public static class ClothesNavigation
    {
        public static async Task ToClothesPage(INavigationService navigationService, string clothesTypeName) =>
             await new NavigationParameters
             {
                 { nameof(clothesTypeName), clothesTypeName }
             }.
             MapAsync(navigationParameters => navigationService.NavigateAsync(nameof(ClothesPage), navigationParameters));
    }
}