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
        /// Параметры инициализации формы
        /// </summary>
        public virtual void Initialize(INavigationParameters parameters)
        { }

        /// <summary>
        /// Параметры перехода формы
        /// </summary>
        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        { }

        /// <summary>
        /// Параметры перехода с формы
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
