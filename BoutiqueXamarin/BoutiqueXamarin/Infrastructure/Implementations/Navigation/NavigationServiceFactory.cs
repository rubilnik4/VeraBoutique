using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation;
using BoutiqueXamarin.Models.Implementations.Navigation.Authorize;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Errors;
using BoutiqueXamarin.Models.Implementations.Navigation.Profiles;
using BoutiqueXamarin.ViewModels.Authorizes;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.ViewModels.Clothes.Choices;
using BoutiqueXamarin.ViewModels.Clothes.Clothes;
using BoutiqueXamarin.ViewModels.Clothes.ClothesDetails;
using BoutiqueXamarin.ViewModels.Errors;
using BoutiqueXamarin.ViewModels.Profiles;
using BoutiqueXamarin.Views.Authorizes;
using BoutiqueXamarin.Views.Base;
using BoutiqueXamarin.Views.Clothes.Choices;
using BoutiqueXamarin.Views.Clothes.Clothes;
using BoutiqueXamarin.Views.Clothes.ClothesDetails;
using BoutiqueXamarin.Views.Errors;
using BoutiqueXamarin.Views.Profiles;
using BoutiqueXamarinCommon.Infrastructure.Interfaces.Authorize;
using Prism.Navigation;
using ReactiveUI.XamForms;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.AuthorizeErrors;
using ResultFunctional.Models.Implementations.Errors.RestErrors;
using ResultFunctional.Models.Interfaces.Errors.Base;
using Xamarin.Forms;

namespace BoutiqueXamarin.Infrastructure.Implementations.Navigation
{
    public class NavigationServiceFactory : INavigationServiceFactory
    {
        public NavigationServiceFactory(INavigationService navigationService, INavigationHistoryService navigationHistoryService,
                                        IBackNavigationService backNavigationService, ILoginService loginService)
        {
            _navigationService = navigationService;
            _navigationHistoryService = navigationHistoryService;
            _backNavigationService = backNavigationService;
            _loginService = loginService;
        }

        /// <summary>
        /// Сервис навигации
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        /// История навигации
        /// </summary>
        private readonly INavigationHistoryService _navigationHistoryService;

        /// <summary>
        /// Сервис навигации назад
        /// </summary>
        private readonly IBackNavigationService _backNavigationService;

        /// <summary>
        /// Сервис авторизации и сохранения логина
        /// </summary>
        private readonly ILoginService _loginService;

        /// <summary>
        /// К стартовой странице
        /// </summary>
        public async Task<INavigationResult> ToInitialPage() =>
            await _navigationService.NavigateAsync(nameof(InitialPage));

        /// <summary>
        /// Перейти к странице авторизации
        /// </summary>
        public async Task<INavigationResult> ToLoginPage() =>
            await new LoginNavigationOptions().
            VoidAsync(_ => _loginService.Logout()).
            MapBindAsync(NavigateTo<LoginPage, LoginViewModel, LoginNavigationOptions>);

        /// <summary>
        /// Перейти к странице регистрации
        /// </summary>
        public async Task<INavigationResult> ToRegisterPage() =>
            await new RegisterNavigationOptions().
            MapAsync(NavigateTo<RegisterPage, RegisterViewModel, RegisterNavigationOptions>);

        /// <summary>
        /// Перейти к странице ошибок
        /// </summary>
        public async Task<INavigationResult> ToErrorPage(IEnumerable<IErrorResult> errors, Func<Task<INavigationResult>> reloadFunc) =>
            await new ErrorNavigationOptions(errors, reloadFunc).
            MapAsync(NavigateTo<ErrorPage, ErrorViewModel, ErrorNavigationOptions>);

        /// <summary>
        /// Перейти назад
        /// </summary>
        public Task<INavigationResult> NavigateBack<TViewModel>(TViewModel viewModel)
            where TViewModel : BaseViewModel =>
            _backNavigationService.NavigateBack(viewModel);

        /// <summary>
        /// Навигация при ошибке
        /// </summary>
        protected async Task<INavigationResult> OnErrorNavigate(IEnumerable<IErrorResult> errors, Func<Task<INavigationResult>> reloadFunc) =>
            errors.First() switch
            {
                AuthorizeErrorResult _ => await ToLoginPage(),
                RestMessageErrorResult { ErrorType: RestErrorType.Unauthorized } => await ToLoginPage(),
                var error => await ToErrorPage(error, reloadFunc),
            };

        /// <summary>
        /// Перейти к странице
        /// </summary>
        protected async Task<INavigationResult> NavigateTo<TPage, TViewModel, TOption>(TOption options)
            where TPage : ReactiveContentPage<TViewModel>
            where TViewModel : NavigationViewModel<TOption>
            where TOption : BaseNavigationOptions =>
            await NavigateFunctions.ToNavigationParameters(options).
            MapAsync(navigationParameters => _navigationService.NavigateAsync(NavigateFunctions.GetPageName<TPage>(), navigationParameters)).
            VoidOkTaskAsync(navigateResult => navigateResult.Success,
                            _ => _navigationHistoryService.EnqueueHistory(options));
    }
}