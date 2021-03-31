using System.Threading.Tasks;
using BoutiqueXamarinCommon.Models.Enums.ViewModels;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultError;
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
    public abstract class BaseViewModel : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        /// <summary>
        /// Состояние модели
        /// </summary>
        protected ViewModelState ViewModelState { get; set; } = ViewModelState.Ok;

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
    }
}
