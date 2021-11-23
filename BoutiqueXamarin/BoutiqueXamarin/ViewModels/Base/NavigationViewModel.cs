using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using BoutiqueXamarin.Infrastructure.Implementations.Navigation;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation;
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
    public abstract class NavigationViewModel<TOptions> : BaseViewModel
        where TOptions : BaseNavigationOptions
    {
        protected NavigationViewModel(IBackNavigationService backNavigationService)
        {
            BackLeftMenuViewModel = new BackLeftMenuViewModel(() => backNavigationService.NavigateBack(this));
        }

        /// <summary>
        /// Меню навигации назад
        /// </summary>
        public BackLeftMenuViewModel BackLeftMenuViewModel { get; }

        /// <summary>
        /// Параметры навигации
        /// </summary>
        private TOptions? _navigationOptions;

        /// <summary>
        /// Параметры навигации
        /// </summary>
        protected TOptions? NavigationOptions
        {
            get => _navigationOptions;
            set => this.RaiseAndSetIfChanged(ref _navigationOptions, value);
        }

        /// <summary>
        /// Параметры инициализации формы с изменением состояния
        /// </summary>
        public override void Initialize(INavigationParameters parameters) =>
            NavigationOptions = GetNavigationOptions(parameters);

        /// <summary>
        /// Преобразовать параметры навигации
        /// </summary>
        private static TOptions GetNavigationOptions(INavigationParameters parameters) =>
            parameters.GetValue<TOptions>(NavigationServiceFactory.GetOptionsName<TOptions>());
    }
}