using System.Reactive.Disposables;
using System.Reactive.Linq;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.ViewModels.Interfaces.Base;
using BoutiqueXamarin.ViewModels.Profiles;
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
    public abstract class NavigationBaseContentPage<TViewModel, TOption> : ReactiveContentPage<TViewModel>
        where TViewModel : NavigationViewModel<TOption>
        where TOption : BaseNavigationOptions
    {
        protected NavigationBaseContentPage()
        {
            this.WhenActivated(disposable =>
            {
                this.WhenAnyValue(x => x.ViewModel!.BackLeftMenuViewModel).
                     WhereNotNull().
                     Where(_ => BackLeftMenuView != null).
                     BindTo(this, x => x.BackLeftMenuView!.ViewModel).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.ViewModel).
                     WhereNotNull().
                     Where(viewModel => viewModel is INavigationProfileViewModel).
                     Select(viewModel => (INavigationProfileViewModel)viewModel).
                     Select(viewModel => viewModel.UserRightMenuViewModel).
                     Where(_ => this.UserRightMenuView != null).
                     BindTo(this, x => x.UserRightMenuView!.ViewModel).
                     DisposeWith(disposable);
            });
        }

        /// <summary>
        /// Меню навигации назад
        /// </summary>
        protected virtual BackLeftMenuView? BackLeftMenuView =>
            null;

        /// <summary>
        /// Меню профиля
        /// </summary>
        protected virtual UserRightMenuView? UserRightMenuView =>
            null;
    }
}