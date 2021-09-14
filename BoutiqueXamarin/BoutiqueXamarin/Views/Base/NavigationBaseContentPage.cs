using System.Reactive.Disposables;
using System.Reactive.Linq;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Base;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.Views.ContentViews;
using BoutiqueXamarin.Views.ContentViews.MenuItems;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms;

namespace BoutiqueXamarin.Views.Base
{
    /// <summary>
    /// Базовый класс страницы с навигацией
    /// </summary>
    public abstract class NavigationBaseContentPage<TViewModel, TParameter, TNavigate> : ReactiveContentPage<TViewModel>
        where TViewModel : NavigationBaseViewModel<TParameter, TNavigate>
        where TParameter : BaseNavigationParameters
        where TNavigate : IBaseNavigationService<TParameter>
    {
        protected NavigationBaseContentPage()
        {
            this.WhenActivated(disposable =>
            {
                this.WhenAnyObservable(x => x.ViewModel!.ErrorViewModelObservable).
                     WhereNotNull().
                     Where(_ => ErrorContentView != null).
                     BindTo(this, x => x.ErrorContentView!.ViewModel).
                     DisposeWith(disposable);

                this.WhenAnyObservable(x => x.ViewModel!.ErrorViewModelObservable).
                    WhereNotNull().
                     Select(x => x.ResultError.HasErrors).
                     Where(_ => ErrorContentView != null).
                     BindTo(this, x => x.ErrorContentView!.IsVisible).
                     DisposeWith(disposable);

                this.WhenAnyObservable(x => x.ViewModel!.ErrorViewModelObservable).
                     WhereNotNull().
                     Select(x => x.ResultError.OkStatus).
                     Where(_ => MainContentView != null).
                     BindTo(this, x => x.MainContentView!.IsVisible).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.ViewModel!.BackLeftMenuViewModel).
                     WhereNotNull().
                     Where(_ => BackLeftMenuView != null).
                     BindTo(this, x => x.BackLeftMenuView!.ViewModel).
                     DisposeWith(disposable);
            });
        }

        /// <summary>
        /// Главное окно
        /// </summary>
        protected virtual ContentView? MainContentView =>
            null;

        /// <summary>
        /// Окно ошибок
        /// </summary>
        protected virtual ErrorConnectionView? ErrorContentView =>
            null;

        /// <summary>
        /// Меню навигации назад
        /// </summary>
        protected virtual BackLeftMenuView? BackLeftMenuView =>
            null;


    }
}