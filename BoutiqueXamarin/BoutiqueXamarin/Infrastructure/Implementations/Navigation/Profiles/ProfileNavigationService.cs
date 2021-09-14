using System.Threading.Tasks;
using BoutiqueXamarin.Infrastructure.Implementations.Navigation.Base;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Authorizes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Profiles;
using BoutiqueXamarin.Models.Implementations.Navigation.Authorize;
using BoutiqueXamarin.Models.Implementations.Navigation.Profiles;
using BoutiqueXamarin.Views.Authorizes;
using BoutiqueXamarin.Views.Profiles;
using Prism.Navigation;

namespace BoutiqueXamarin.Infrastructure.Implementations.Navigation.Profiles
{
    /// <summary>
    /// Сервис навигации к странице информации о пользователе
    /// </summary>
    public class ProfileNavigationService : BaseNavigationService<ProfileNavigationParameters, ProfilePage>, IProfileNavigationService
    {
        public ProfileNavigationService(INavigationService navigationService)
            : base(navigationService)
        { }

        /// <summary>
        /// Перейти к странице
        /// </summary>
        public async Task NavigateTo() =>
            await NavigateTo(new ProfileNavigationParameters());
    }
}