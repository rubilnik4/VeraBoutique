using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using BoutiqueXamarin.Infrastructure.Implementations.Navigation.Base;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Base;
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
using Xamarin.Forms;

namespace BoutiqueXamarin.ViewModels.Base
{
    /// <summary>
    /// Базовая модель с навигацией
    /// </summary>
    public abstract class NavigationBaseViewModel<TParameter, TNavigate> : BaseViewModel
        where TParameter : EmptyNavigationParameters
        where TNavigate: IBaseNavigationService<TParameter>
    {
        protected NavigationBaseViewModel(TNavigate navigateService)
        {
            NavigateService = navigateService;
            InitializeErrorsObservable = Observable.Return((IResultError)new ResultError()).
                                         ToProperty(this, nameof(InitializeErrors));
            NavigateBackCommand = ReactiveCommand.CreateFromTask(_ => NavigateService.NavigateBack());
        }

        /// <summary>
        /// Сервис навигации
        /// </summary>
        public TNavigate NavigateService { get; }

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
        /// Ошибки при инициализации
        /// </summary>
        protected virtual ObservableAsPropertyHelper<IResultError> InitializeErrorsObservable { get; }

        /// <summary>
        /// Ошибки при инициализации
        /// </summary>
        public IResultError InitializeErrors =>
            InitializeErrorsObservable.Value;

        /// <summary>
        /// Команда. Вернуться назад
        /// </summary>
        public ReactiveCommand<Unit, Unit> NavigateBackCommand { get; }

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