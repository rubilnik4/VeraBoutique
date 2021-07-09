using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using BoutiqueXamarin.ViewModels.Clothes.Clothes.ClothesViewModelItems;
using BoutiqueXamarin.Views.Clothes.Choices;
using ReactiveUI;
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

            var tapGestureRecognizer = new TapGestureRecognizer();
            this.FilterButton.GestureRecognizers.Add(tapGestureRecognizer);

            this.WhenActivated(disposable =>
                {
                    this.OneWayBind(ViewModel, x => x.ClothesViewModelColumnItems, x => x.ClothesColumns.ItemsSource).
                         DisposeWith(disposable);

                    this.OneWayBind(ViewModel, x => x.FilterViewModel, x => x.FilterViewControl.ViewModel).
                         DisposeWith(disposable);

                    ClothesColumns.
                        Events().ItemAppearing.
                        SelectMany(item => ViewModel!.ClothesViewModelColumnItems[item.ItemIndex].ClothesViewModelItems.
                                           Where(clothesItems => clothesItems?.ImageLoad == false)).
                        Subscribe(clothesItem => clothesItem?.ImageCommand.Execute(Unit.Default).Subscribe()).
                        DisposeWith(disposable);

                    tapGestureRecognizer.
                        Events().Tapped.
                        Subscribe(_ => SideMenuView.State = SideMenuState.RightMenuShown).
                        DisposeWith(disposable);

                    this.FilterViewControl.FilterHideButton.
                         Events().Clicked.
                         Subscribe(_ => SideMenuView.State = SideMenuState.MainViewShown).
                         DisposeWith(disposable);
                });
        }
    }
}