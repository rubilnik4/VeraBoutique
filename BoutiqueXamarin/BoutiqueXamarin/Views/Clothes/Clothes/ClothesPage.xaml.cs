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

                    this.WhenAnyValue(x => x.ViewModel!.FilterSizeViewModelItems).
                         Where(filterSizes => filterSizes != null).
                         Subscribe(filterSizes => BindableLayout.SetItemsSource(FilterSizes, filterSizes)).
                         DisposeWith(disposable);

                    this.WhenAnyValue(x => x.ViewModel!.FilterColorViewModelItems).
                         Where(filterColors => filterColors != null).
                         Subscribe(filterColors => BindableLayout.SetItemsSource(FilterColors, filterColors)).
                         DisposeWith(disposable);

                    this.OneWayBind(ViewModel, x => x.FilterPriceViewModelItem, x => x.FilterPrices.ViewModel).
                         DisposeWith(disposable);

                    tapGestureRecognizer.
                        Events().Tapped.
                        Subscribe(_ => SideMenuView.State = SideMenuState.RightMenuShown).
                        DisposeWith(disposable);

                    FilterHide.
                        Events().Clicked.
                        Subscribe(_ => SideMenuView.State = SideMenuState.MainViewShown).
                        DisposeWith(disposable);
                });
        }
    }
}