using BoutiqueXamarin.ViewModels.Clothes.Choices;
using BoutiqueXamarin.ViewModels.Clothes.Clothes;
using BoutiqueXamarin.Views;
using BoutiqueXamarin.Views.Clothes;
using BoutiqueXamarin.Views.Clothes.Choices;
using BoutiqueXamarin.Views.Clothes.Clothes;
using Prism.Ioc;
using Xamarin.Forms;

namespace BoutiqueXamarin.DependencyInjection
{
    /// <summary>
    /// Регистрация видов и моделей
    /// </summary>
    public static class PagesRegistration
    {
        /// <summary>
        /// Регистрация видов и моделей
        /// </summary>
        public static void RegisterPages(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<ChoicePage, ChoiceViewModel>();
            containerRegistry.RegisterForNavigation<ClothesPage, ClothesViewModel>();
            containerRegistry.RegisterForNavigation<ClothesDetailPage, ClothesDetailViewModel>();
        }
    }
}