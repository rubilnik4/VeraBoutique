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

namespace BoutiqueXamarin.ViewModels.Base
{
    /// <summary>
    /// Базовая модель с навигацией
    /// </summary>
    public abstract class NavigationBaseViewModel<TParameter> : BaseViewModel
        where TParameter : BaseNavigationParameters
    {
        /// <summary>
        /// Параметры инициализации формы с изменением состояния
        /// </summary>
        public override async void Initialize(INavigationParameters parameters) =>
            await new ResultError().
            ResultErrorVoidOk(() => ViewModelState = ViewModelState.Loading).
            ToResultBindValue(GetNavigationParameter(parameters)).
            ResultValueBindErrorsOkAsync(InitializeAction).
            ResultValueVoidOkBadTaskAsync(
                actionOk: _ => ViewModelState = ViewModelState.Ok,
                actionBad: _ => ViewModelState = ViewModelState.Error);

        /// <summary>
        /// Асинхронная загрузка параметров модели
        /// </summary>
        protected abstract Task<IResultError> InitializeAction(TParameter navigateParameter);

        /// <summary>
        /// Преобразовать параметры навигации
        /// </summary>
        private static IResultValue<TParameter> GetNavigationParameter(INavigationParameters parameters) =>
            parameters.GetValue<TParameter>(NavigationParametersInfo.GetNavigationParameterName<TParameter>()).
            ToResultValueNullCheck(new ErrorResult(ErrorResultType.ValueNotFound,
                                                   $"Параметры навигации [{typeof(TParameter).Name}] не найдены"));
    }
}