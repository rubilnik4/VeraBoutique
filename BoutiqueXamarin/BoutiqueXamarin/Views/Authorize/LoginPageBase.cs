using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Authorize;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Authorize;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.ViewModels.Authorize;
using BoutiqueXamarin.ViewModels.Clothes.Clothes;
using BoutiqueXamarin.Views.Base;

namespace BoutiqueXamarin.Views.Authorize
{
    /// <summary>
    /// Авторизация
    /// </summary>
    public abstract class LoginPageBase : NavigationBaseContentPage<LoginViewModel, LoginNavigationParameters, ILoginNavigationService>
    { }
}