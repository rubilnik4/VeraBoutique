using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using BoutiqueXamarin.Infrastructure.Implementations.Navigation.Base;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Authorizes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Base;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Errors;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using BoutiqueXamarin.ViewModels.Base.MenuItems;
using BoutiqueXamarinCommon.Infrastructure.Implementations.Authorize;
using BoutiqueXamarinCommon.Models.Enums.ViewModels;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using Prism.Navigation;
using ReactiveUI;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;
using Xamarin.Forms;

namespace BoutiqueXamarin.ViewModels.Base
{
    /// <summary>
    /// Базовая модель с навигацией
    /// </summary>
    public abstract class NavigationViewModel<TOptions, TNavigate> : BaseViewModel
        where TOptions : BaseNavigationOptions
        where TNavigate : IBaseNavigationService<TOptions>
    {
        protected NavigationViewModel(TNavigate navigateService)
        {
            BackLeftMenuViewModel = new BackLeftMenuViewModel(navigateService);
        }

        /// <summary>
        /// Необходимость авторизации
        /// </summary>
        protected virtual bool Authorize => 
            false;

        /// <summary>
        /// Меню навигации назад
        /// </summary>
        public BackLeftMenuViewModel BackLeftMenuViewModel { get; }

        /// <summary>
        /// Параметры навигации
        /// </summary>
        private TOptions? _navigationParameters;

        /// <summary>
        /// Параметры навигации
        /// </summary>
        protected TOptions? NavigationParameters
        {
            get => _navigationParameters;
            set => this.RaiseAndSetIfChanged(ref _navigationParameters, value);
        }

        /// <summary>
        /// Параметры инициализации формы с изменением состояния
        /// </summary>
        public override void Initialize(INavigationParameters parameters) =>
            NavigationParameters = GetNavigationOptions(parameters);

        /// <summary>
        /// Проверка авторизации
        /// </summary>
        private TOptions ValidateAuthorize(TOptions navigateOptions) =>
            navigateOptions.
            VoidOk(options => Authorize && !ValidateAuthorizeOption(navigateOptions),
                   options => options.ToString());

        /// <summary>
        /// Проверка авторизации параметров
        /// </summary>
        private static bool ValidateAuthorizeOption(TOptions navigateOptions) =>
            navigateOptions is AuthorizeBaseNavigationOptions authorizeOptions &&
            TokenValidate.IsTokenValid(authorizeOptions.Token);

        /// <summary>
        /// Преобразовать параметры навигации
        /// </summary>
        private static TOptions GetNavigationOptions(INavigationParameters parameters) =>
            parameters.GetValue<TOptions>(NavigationOptionsInfo.GetNavigationParameterName<TOptions>());
    }
}