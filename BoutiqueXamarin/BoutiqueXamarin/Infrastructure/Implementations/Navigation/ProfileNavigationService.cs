using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation;
using BoutiqueXamarin.Models.Implementations.Navigation.Profiles;
using BoutiqueXamarin.ViewModels.Profiles;
using BoutiqueXamarin.Views.Profiles;
using BoutiqueXamarinCommon.Infrastructure.Interfaces.Authorize;
using Prism.Navigation;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;

namespace BoutiqueXamarin.Infrastructure.Implementations.Navigation
{
    public class ProfileNavigationService: NavigationServiceFactory, IProfileNavigationService
    {
        public ProfileNavigationService(INavigationService navigationService, ILoginService loginService, 
                                        IProfileRestService profileRestService)
            :base(navigationService, loginService)
        {
            _profileRestService = profileRestService;
        }

        /// <summary>
        /// Сервис личной информации
        /// </summary>
        private readonly IProfileRestService _profileRestService;

        /// <summary>
        /// Перейти к странице личной информации
        /// </summary>
        public async Task<INavigationResult> ToProfilePage() =>
            await _profileRestService.GetProfile().
            ResultValueToValueOkBadBindAsync(ToProfilePage,
                                             OnErrorNavigate);

        /// <summary>
        /// Перейти к странице личной информации
        /// </summary>
        private async Task<INavigationResult> ToProfilePage(IBoutiqueUserDomain user) =>
            await new ProfileNavigationOptions(user).
            MapAsync(NavigateTo<ProfilePage, ProfileViewModel, ProfileNavigationOptions>);
    }
}