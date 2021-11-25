using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation;
using BoutiqueXamarin.Models.Implementations.Navigation.Errors;
using BoutiqueXamarin.ViewModels.Errors;
using BoutiqueXamarin.Views.Base;
using BoutiqueXamarin.Views.Errors;
using BoutiqueXamarinCommon.Infrastructure.Interfaces.Authorize;
using Prism.Navigation;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace BoutiqueXamarin.Infrastructure.Implementations.Navigation
{
    public class DefaultNavigationService : NavigationServiceFactory, IDefaultNavigationService
    {
        public DefaultNavigationService(INavigationService navigationService, INavigationHistoryService navigationHistoryService,
                                        IBackNavigationService backNavigationService, ILoginService loginService)
            : base(navigationService, navigationHistoryService, backNavigationService, loginService)
        {
            _navigationService = navigationService;
        }

        /// <summary>
        /// Сервис навигации
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        /// К стартовой странице
        /// </summary>
        public async Task<INavigationResult> ToInitialPage() =>
            await _navigationService.NavigateAsync(nameof(InitialPage));
    }
}