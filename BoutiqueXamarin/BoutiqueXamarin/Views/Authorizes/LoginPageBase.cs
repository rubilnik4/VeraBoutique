using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Authorizes;
using BoutiqueXamarin.Models.Implementations.Navigation.Authorize;
using BoutiqueXamarin.ViewModels.Authorizes;
using BoutiqueXamarin.Views.Base;

namespace BoutiqueXamarin.Views.Authorizes
{
    /// <summary>
    /// Авторизация
    /// </summary>
    public abstract class LoginPageBase : NavigationBaseContentPage<LoginViewModel, LoginNavigationParameters, ILoginNavigationService>
    { }
}