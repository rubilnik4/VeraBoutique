using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using BoutiqueXamarin.Views.Clothes.Clothes.ClothesViewItems.ClothesFilters;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.Clothes.Clothes.ClothesViewItems.ClothesSorting
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SortingView : SortingViewBase
    {
        public SortingView()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                //this.WhenAnyValue(x => x.ViewModel!.FilterSizeViewModelItems).
                //     Where(filterSizes => filterSizes != null).
                //     Subscribe(filterSizes => BindableLayout.SetItemsSource(FilterSizes, filterSizes)).
                //     DisposeWith(disposable);

                //this.WhenAnyValue(x => x.ViewModel!.FilterColorViewModelItems).
                //     Where(filterColors => filterColors != null).
                //     Subscribe(filterColors => BindableLayout.SetItemsSource(FilterColors, filterColors)).
                //     DisposeWith(disposable);

                //this.OneWayBind(ViewModel, x => x.FilterPriceViewModelItem, x => x.FilterPrices.ViewModel).
                //     DisposeWith(disposable);
            });
        }

        /// <summary>
        /// Событие закрытия формы
        /// </summary>
        public IObservable<EventArgs> SortingHideButtonClick =>
            SortingHide.Events().Clicked;
    }
}