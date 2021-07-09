using System.Threading.Tasks;
using BoutiqueXamarin.Infrastructure.Implementations.Navigation.Base;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using BoutiqueXamarinCommon.Models.Enums.ViewModels;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Prism.Navigation;
using ReactiveUI;

namespace BoutiqueXamarin.ViewModels.Base
{
    /// <summary>
    /// Базовая модель с навигацией
    /// </summary>
    public abstract class NavigationBaseViewModel<TParameter> : BaseViewModel
        where TParameter : EmptyNavigationParameters
    {
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
            parameters.GetValue<TParameter>(NavigationParametersInfo.GetNavigationParameterName<TParameter>());
    }
}