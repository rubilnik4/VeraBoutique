using BoutiqueXamarin.ViewModels.Authorizes;
using BoutiqueXamarin.ViewModels.Clothes.Choices;
using BoutiqueXamarin.ViewModels.Clothes.Clothes;
using BoutiqueXamarin.ViewModels.Clothes.ClothesDetails;
using BoutiqueXamarin.ViewModels.Errors;
using BoutiqueXamarin.ViewModels.Profiles;
using BoutiqueXamarin.Views;
using BoutiqueXamarin.Views.Authorizes;
using BoutiqueXamarin.Views.Clothes;
using BoutiqueXamarin.Views.Clothes.Choices;
using BoutiqueXamarin.Views.Clothes.Clothes;
using BoutiqueXamarin.Views.Clothes.ClothesDetails;
using BoutiqueXamarin.Views.Errors;
using BoutiqueXamarin.Views.Profiles;
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
            containerRegistry.RegisterForNavigation<ErrorPage, ErrorViewModel>();
            containerRegistry.RegisterForNavigation<ChoicePage, ChoiceViewModel>();
            containerRegistry.RegisterForNavigation<ClothesPage, ClothesViewModel>();
            containerRegistry.RegisterForNavigation<ClothesDetailPage, ClothesDetailViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterViewModel>();
            containerRegistry.RegisterForNavigation<ProfilePage, ProfileViewModel>();
        }
    }
}