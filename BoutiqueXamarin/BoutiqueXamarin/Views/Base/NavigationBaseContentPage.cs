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
    /// Базовый класс страницы с навигацией
    /// </summary>
    public abstract class NavigationBaseContentPage<TViewModel, TParameter, TNavigate> : ErrorBaseContentPage<TViewModel>
        where TViewModel : NavigationBaseViewModel<TParameter, TNavigate>
        where TParameter : EmptyNavigationParameters
        where TNavigate : IBaseNavigationService<TParameter>
    {
        protected NavigationBaseContentPage()
        {
            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, x => x.BackLeftMenuViewModel, _ => BackLeftMenuView.ViewModel).
                     DisposeWith(disposable);
            });
        }

        /// <summary>
        /// Меню навигации назад
        /// </summary>
        protected abstract BackLeftMenuView BackLeftMenuView { get; }
    }
}