using BoutiqueXamarinCommon.Models.Enums.ViewModels;
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
        /// Параметры инициализации формы с изменением состояния
        /// </summary>
        public virtual void Initialize(INavigationParameters parameters)
        { }
            //await new ResultError().
            //ResultErrorVoidOk(() => ViewModelState = ViewModelState.Loading).
            //ResultErrorBindOkAsync(InitializeAction).
            //ResultErrorVoidOkBadTaskAsync(
            //    actionOk: () => ViewModelState = ViewModelState.Ok,
            //    actionBad: _ => ViewModelState = ViewModelState.Error);

        /// <summary>
        /// Параметры перехода c формы с изменением состояния
        /// </summary>
        public virtual void OnNavigatedFrom(INavigationParameters parameters) 
        { }

        /// <summary>
        /// Параметры перехода на форму с изменением состояния
        /// </summary>
        public virtual void OnNavigatedTo(INavigationParameters parameters)
        { }

        /// <summary>
        /// Параметры закрытия формы
        /// </summary>
        public virtual void Destroy()
        { }
    }
}
