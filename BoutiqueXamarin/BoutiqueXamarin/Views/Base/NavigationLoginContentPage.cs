using System.Reactive.Disposables;
using System.Reactive.Linq;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Base;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.Views.ContentViews.MenuItems;
using ReactiveUI;
using ReactiveUI.XamForms;

namespace BoutiqueXamarin.Views.Base
{
    /// <summary>
    /// Базовый класс страницы с навигацией и авторизацией
    /// </summary>
    public class NavigationLoginContentPage<TViewModel, TParameter, TNavigate> : NavigationBaseContentPage<TViewModel, TParameter, TNavigate>
        where TViewModel : NavigationLoginViewModel<TParameter, TNavigate>
        where TParameter : BaseNavigationOptions
        where TNavigate : IBaseNavigationService<TParameter>
    {
        protected NavigationLoginContentPage()
        {
            this.WhenActivated(disposable =>
            {
                this.WhenAnyValue(x => x.ViewModel!.UserRightMenuViewModel).
                     WhereNotNull().
                     Where(_ => UserRightMenuView != null).
                     BindTo(this, x => x.UserRightMenuView!.ViewModel).
                     DisposeWith(disposable);
            });
        }

        /// <summary>
        /// Меню навигации назад
        /// </summary>
        protected virtual UserRightMenuView? UserRightMenuView =>
            null;
    }
}