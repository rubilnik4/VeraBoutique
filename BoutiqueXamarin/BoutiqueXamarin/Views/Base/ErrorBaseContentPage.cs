using System.Reactive.Disposables;
using System.Reactive.Linq;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.ViewModels.Clothes.Choices;
using BoutiqueXamarin.Views.ContentViews;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms;

namespace BoutiqueXamarin.Views.Base
{
    /// <summary>
    /// Базовый класс страницы
    /// </summary>
    public abstract class ErrorBaseContentPage<TViewModel> : ReactiveContentPage<TViewModel>
        where TViewModel: ErrorBaseViewModel
    {
        protected ErrorBaseContentPage()
        {
            this.WhenActivated(disposable =>
            {
                this.ViewModel!.ErrorViewModelObservable.
                     WhereNotNull().
                     BindTo(this, x => x.ErrorContentView.ViewModel).
                     DisposeWith(disposable);

                this.ViewModel!.ErrorViewModelObservable.
                     WhereNotNull().
                     Select(x => x.ResultError.HasErrors).
                     BindTo(this, x => x.ErrorContentView.IsVisible).
                     DisposeWith(disposable);

                this.ViewModel!.ErrorViewModelObservable.
                     WhereNotNull().
                     Select(x => x.ResultError.OkStatus).
                     BindTo(this, x => x.MainContentView.IsVisible).
                     DisposeWith(disposable);
            });
        }

        /// <summary>
        /// Главное окно
        /// </summary>
        protected abstract ContentView MainContentView { get; }

        /// <summary>
        /// Окно ошибок
        /// </summary>
        protected abstract ErrorConnectionView ErrorContentView { get; }
    }
}