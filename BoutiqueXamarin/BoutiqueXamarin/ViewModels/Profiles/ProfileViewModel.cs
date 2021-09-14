using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Authorizes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Profiles;
using BoutiqueXamarin.Models.Implementations.Navigation.Authorize;
using BoutiqueXamarin.Models.Implementations.Navigation.Profiles;
using BoutiqueXamarin.ViewModels.Base;

namespace BoutiqueXamarin.ViewModels.Profiles
{
    /// <summary>
    /// Модель информации о пользователе
    /// </summary>
    public class ProfileViewModel : NavigationBaseViewModel<ProfileNavigationParameters, IProfileNavigationService>
    {
        public ProfileViewModel(IProfileNavigationService profileNavigationService)
         : base(profileNavigationService)
        { }
    }
}