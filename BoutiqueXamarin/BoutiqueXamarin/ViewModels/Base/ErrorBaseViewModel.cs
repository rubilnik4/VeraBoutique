using System;
using System.Reactive.Linq;

namespace BoutiqueXamarin.ViewModels.Base
{
    /// <summary>
    /// Базовая модель ошибок
    /// </summary>
    public abstract class ErrorBaseViewModel : BaseViewModel
    {
        protected ErrorBaseViewModel()
        {
            ErrorViewModelObservable = Observable.Return(ErrorConnectionViewModel.EmptyErrorConnectionViewModel);
        }

        /// <summary>
        /// Ошибки при инициализации
        /// </summary>
        public virtual IObservable<ErrorConnectionViewModel> ErrorViewModelObservable { get; }
    }
}