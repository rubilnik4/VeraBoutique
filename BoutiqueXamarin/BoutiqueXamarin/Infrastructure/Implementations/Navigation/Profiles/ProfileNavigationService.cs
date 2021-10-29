using System.Threading.Tasks;
using BoutiqueXamarin.Infrastructure.Implementations.Navigation.Base;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Authorizes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Profiles;
using BoutiqueXamarin.Models.Implementations.Navigation.Authorize;
using BoutiqueXamarin.Models.Implementations.Navigation.Profiles;
using BoutiqueXamarin.Views.Authorizes;
using BoutiqueXamarin.Views.Profiles;
using BoutiqueXamarinCommon.Infrastructure.Interfaces.Authorize;
using Prism.Navigation;
using ResultFunctional.FunctionalExtensions.Async;

namespace BoutiqueXamarin.Infrastructure.Implementations.Navigation.Profiles
{
    /// <summary>
    /// Сервис навигации к странице информации о пользователе
    /// </summary>
    public class ProfileNavigationService : BaseNavigationService<ProfileNavigationOptions, ProfilePage>, IProfileNavigationService
    {
        private readonly ILoginStore _loginStore;

        public ProfileNavigationService(INavigationService navigationService, ILoginStore loginStore)
            : base(navigationService)
        {
            _loginStore = loginStore;
        }

        /// <summary>
        /// Перейти к странице
        /// </summary>
        public async Task<INavigationResult> NavigateTo() =>
            await _loginStore.GetTokenValue().
            MapTaskAsync(token => new ProfileNavigationOptions(token)).
            MapBindAsync(NavigateTo);
    }
}