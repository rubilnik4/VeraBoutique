using BoutiqueXamarin.ViewModels;
using BoutiqueXamarin.ViewModels.Clothes.Choice;
using BoutiqueXamarin.Views;
using BoutiqueXamarin.Views.Clothes;
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
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<ChoicePage, ChoiceViewModel>();
        }
    }
}