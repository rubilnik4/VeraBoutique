using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Authorizes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Profiles;
using BoutiqueXamarin.Models.Implementations.Navigation.Authorize;
using BoutiqueXamarin.Models.Implementations.Navigation.Profiles;
using BoutiqueXamarin.ViewModels.Authorizes;
using BoutiqueXamarin.ViewModels.Profiles;
using BoutiqueXamarin.Views.Base;

namespace BoutiqueXamarin.Views.Profiles
{
    /// <summary>
    /// Информация о профиле
    /// </summary>
    public abstract class ProfilePageBase : NavigationBaseContentPage<ProfileViewModel, ProfileNavigationParameters, IProfileNavigationService>
    { }
}