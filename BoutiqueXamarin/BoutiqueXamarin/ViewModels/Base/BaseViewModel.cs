using System;
using System.Reactive.Linq;
using Prism.Navigation;
using ReactiveUI;

namespace BoutiqueXamarin.ViewModels.Base
{
    /// <summary>
    /// Базовая модель отображения
    /// </summary>
    public abstract class BaseViewModel : ReactiveObject, IInitialize, INavigationAware, IDestructible
    {
        /// <summary>
        /// Параметры инициализации формы с изменением состояния
        /// </summary>
        public virtual void Initialize(INavigationParameters parameters)
        { }
        
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

        /// <summary>
        /// Статус загрузки
        /// </summary>
        public bool IsLoaded { get; protected set; }
    }
}
