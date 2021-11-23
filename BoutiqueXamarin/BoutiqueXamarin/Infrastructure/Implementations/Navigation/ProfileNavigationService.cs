using System.Diagnostics;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Profiles;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.ViewModels.Clothes.Choices;
using BoutiqueXamarin.ViewModels.Profiles;
using BoutiqueXamarin.Views.Clothes.Choices;
using BoutiqueXamarin.Views.Profiles;
using BoutiqueXamarinCommon.Infrastructure.Interfaces.Authorize;
using Prism.Navigation;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;

namespace BoutiqueXamarin.Infrastructure.Implementations.Navigation
{
    /// <summary>
    /// Навигация к странице пользовательской информации
    /// </summary>
    public class ProfileNavigationService : NavigationServiceFactory, IProfileNavigationService
    {
        public ProfileNavigationService(INavigationService navigationService, INavigationHistoryService navigationHistoryService,
                                        ILoginService loginService, IProfileRestService profileRestService)
            : base(navigationService, navigationHistoryService, loginService)
        {
            _navigationService = navigationService;
            _profileRestService = profileRestService;
        }

        /// <summary>
        /// Сервис навигации
        /// </summary>
        private readonly INavigationService _navigationService;

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
                                             errors => OnErrorNavigate(errors, ToProfilePage));

        /// <summary>
        /// Перейти к странице личных данных
        /// </summary>
        public async Task<INavigationResult> ToPersonalPage(IBoutiqueUserDomain user) =>
            await new PersonalNavigationOptions(user).
            MapAsync(NavigateTo<PersonalPage, PersonalViewModel, PersonalNavigationOptions>);

        /// <summary>
        /// Перейти к странице личной информации
        /// </summary>
        private async Task<INavigationResult> ToProfilePage(IBoutiqueUserDomain user) =>
            await new ProfileNavigationOptions(user).
            MapAsync(NavigateTo<ProfilePage, ProfileViewModel, ProfileNavigationOptions>);
    }
}