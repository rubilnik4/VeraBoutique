using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.Clothes.Clothes.ClothesViewItems.ClothesFilters
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterView : FilterViewBase
    {
        public FilterView()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
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
            });
        }

        /// <summary>
        /// Событие закрытия формы
        /// </summary>
        public IObservable<EventArgs> FilterHideButtonClick =>
            FilterHide.Events().Clicked;
    }
}