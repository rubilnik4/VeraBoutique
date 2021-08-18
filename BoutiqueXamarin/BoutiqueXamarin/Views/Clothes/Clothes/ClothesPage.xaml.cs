using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.ViewModels.Base.MenuItems;
using BoutiqueXamarin.ViewModels.Clothes.Clothes.ClothesViewModelItems;
using BoutiqueXamarin.Views.Clothes.Choices;
using BoutiqueXamarin.Views.ContentViews;
using BoutiqueXamarin.Views.ContentViews.MenuItems;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.Clothes.Clothes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClothesPage : ClothesPageBase
    {
        public ClothesPage()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
                {
                    this.OneWayBind(ViewModel, x => x.ClothesViewModelColumnItems, x => x.ClothesColumns.ItemsSource).
                         DisposeWith(disposable);

                    this.OneWayBind(ViewModel, x => x.SortingViewModel, x => x.SortingViewControl.ViewModel).
                         DisposeWith(disposable);

                    this.OneWayBind(ViewModel, x => x.FilterViewModel, x => x.FilterViewControl.ViewModel).
                         DisposeWith(disposable);

                    ClothesColumns.
                        Events().ItemAppearing.
                        SelectMany(item => ViewModel!.ClothesViewModelColumnItems[item.ItemIndex].ClothesViewModelItems.
                                           Where(clothesItems => clothesItems?.ImageLoad == false)).
                        Subscribe(clothesItem => clothesItem?.ImageCommand.Execute(Unit.Default).Subscribe()).
                        DisposeWith(disposable);

                    this.SortButton.Tapped.
                         Subscribe(_ =>
                            {
                                SideMenuView.State = SideMenuState.RightMenuShown;
                                FilterViewControl.IsVisible = false;
                                SortingViewControl.IsVisible = true;
                            }).
                         DisposeWith(disposable);

                    this.FilterButton.Tapped.
                         Subscribe(_ =>
                            {
                                SideMenuView.State = SideMenuState.RightMenuShown;
                                FilterViewControl.IsVisible = true;
                                SortingViewControl.IsVisible = false;
                            }).
                         DisposeWith(disposable);

                    this.FilterViewControl.FilterHideButtonClick.
                         Merge(SortingViewControl.SortingHideButtonClick).
                         Subscribe(_ =>
                         {
                             SideMenuView.State = SideMenuState.MainViewShown;
                             FilterViewControl.IsVisible = false;
                             SortingViewControl.IsVisible = false;
                         }).
                         DisposeWith(disposable);

                    this.WhenAnyValue(x => x.SideMenuView.State).
                         Where(state => state == SideMenuState.MainViewShown).
                         Subscribe(_ =>
                         {
                             FilterViewControl.IsVisible = false;
                             SortingViewControl.IsVisible = false;
                         }).
                         DisposeWith(disposable);

                    this.BindCommand(ViewModel, x => x.ChoiceNavigateCommand, x => x.MenuButton).
                         DisposeWith(disposable);
                    this.OneWayBind(ViewModel, x => x.UserRightMenuViewModel, x => x.UserRightMenuView.ViewModel).
                         DisposeWith(disposable);
                });
        }

        /// <summary>
        /// Главное окно
        /// </summary>
        protected override ContentView MainContentView =>
            this.MainView;

        /// <summary>
        /// Окно ошибок
        /// </summary>
        protected override ErrorConnectionView ErrorContentView =>
            this.ErrorView;

        /// <summary>
        /// Главное окно
        /// </summary>
        protected override BackLeftMenuView BackLeftMenuView =>
            this.BackLeftMenu;

        /// <summary>
        /// Меню навигации назад
        /// </summary>
        protected override UserRightMenuView UserRightMenuView =>
            this.UserRightMenu;
    }
}