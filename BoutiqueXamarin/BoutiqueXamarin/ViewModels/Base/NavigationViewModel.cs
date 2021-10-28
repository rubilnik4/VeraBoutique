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
using BoutiqueXamarinCommon.Models.Enums.ViewModels;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using Prism.Navigation;
using ReactiveUI;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;
using Xamarin.Forms;

namespace BoutiqueXamarin.ViewModels.Base
{
    /// <summary>
    /// Базовая модель с навигацией
    /// </summary>
    public abstract class NavigationViewModel<TParameter, TNavigate> : BaseViewModel
        where TParameter : BaseNavigationOptions
        where TNavigate : IBaseNavigationService<TParameter>
    {
        protected NavigationViewModel(TNavigate navigateService)
        {
            BackLeftMenuViewModel = new BackLeftMenuViewModel(navigateService);
        }

        /// <summary>
        /// Меню навигации назад
        /// </summary>
        public BackLeftMenuViewModel BackLeftMenuViewModel { get; }

        /// <summary>
        /// Параметры навигации
        /// </summary>
        private TParameter? _navigationParameters;

        /// <summary>
        /// Параметры навигации
        /// </summary>
        protected TParameter? NavigationParameters
        {
            get => _navigationParameters;
            set => this.RaiseAndSetIfChanged(ref _navigationParameters, value);
        }

        /// <summary>
        /// Параметры инициализации формы с изменением состояния
        /// </summary>
        public override void Initialize(INavigationParameters parameters) =>
            NavigationParameters = GetNavigationParameters(parameters);

        /// <summary>
        /// Преобразовать параметры навигации
        /// </summary>
        private static TParameter GetNavigationParameters(INavigationParameters parameters) =>
            parameters.GetValue<TParameter>(NavigationOptionsInfo.GetNavigationParameterName<TParameter>());
    }
}