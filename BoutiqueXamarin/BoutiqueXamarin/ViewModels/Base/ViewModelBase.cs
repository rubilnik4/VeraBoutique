using System.Threading.Tasks;
using BoutiqueCommon.Extensions.TaskExtensions;
using BoutiqueXamarin.Models.Enums.ViewModels;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultError;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Prism.Mvvm;
using Prism.Navigation;

namespace BoutiqueXamarin.ViewModels.Base
{
    /// <summary>
    /// Базовая модель отображения
    /// </summary>
    public abstract class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        protected ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        /// <summary>
        /// Сервис навигации
        /// </summary>
        protected INavigationService NavigationService { get; }

        /// <summary>
        /// Заголовок
        /// </summary>
        public abstract string Title { get; }

        /// <summary>
        /// Состояние модели
        /// </summary>
        protected ViewModelState ViewModelState { get; private set; } = ViewModelState.Ok;

        /// <summary>
        /// Параметры инициализации формы
        /// </summary>
        public virtual void Initialize(INavigationParameters parameters)
        { }

        /// <summary>
        /// Параметры перехода формы
        /// </summary>
        public async void OnNavigatedFrom(INavigationParameters parameters) =>
            await new ResultError().
            ResultErrorVoidOk(() => ViewModelState = ViewModelState.Loading).
            ResultErrorBindOkAsync(NavigatedFromAction).
            ResultErrorVoidOkBadAsync(
                actionOk:() => ViewModelState = ViewModelState.Ok,
                actionBad:_ => ViewModelState = ViewModelState.Error);
           

        /// <summary>
        /// Параметры перехода с формы
        /// </summary>
        public void OnNavigatedTo(INavigationParameters parameters)
        { }

        /// <summary>
        /// Параметры закрытия формы
        /// </summary>
        public virtual void Destroy()
        { }

        protected abstract Task<IResultError> NavigatedFromAction();
    }
}
