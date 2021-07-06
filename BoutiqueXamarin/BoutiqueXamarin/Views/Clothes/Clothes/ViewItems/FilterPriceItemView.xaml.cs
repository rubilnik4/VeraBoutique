using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.Clothes.Clothes.ViewItems
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterPriceItemView : FilterPriceItemBase
    {
        public FilterPriceItemView()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel, x => x.PriceMinimumValue, x => x.PriceSlider.LowerValue).
                     DisposeWith(disposable);
                this.Bind(ViewModel, x => x.PriceMaximumValue, x => x.PriceSlider.UpperValue).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.ViewModel!.PriceMinimum).
                     Subscribe(priceMinimum => PriceSlider.MinimumValue = (double)priceMinimum).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.ViewModel!.PriceMaximum).
                     Subscribe(priceMaximum => PriceSlider.MaximumValue = (double)priceMaximum).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.ViewModel!.PriceMinimum).
                     Subscribe(priceMinimum => PriceSlider.LowerValue = (double)priceMinimum).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.ViewModel!.PriceMaximum).
                     Subscribe(priceMaximum => PriceSlider.UpperValue = (double)priceMaximum).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.ViewModel!.PriceStep).
                     Subscribe(step => PriceSlider.StepValue = step).
                     DisposeWith(disposable);

                Observable.FromEventPattern(PriceSlider, nameof(PriceSlider.LowerDragCompleted)).
                           Throttle(TimeSpan.FromSeconds(1)).
                           Select(_ => Unit.Default).
                           InvokeCommand(this, x => x.ViewModel!.ClothesFilterCommand).
                           DisposeWith(disposable);

                Observable.FromEventPattern(PriceSlider, nameof(PriceSlider.UpperDragCompleted)).
                           Throttle(TimeSpan.FromSeconds(1)).
                           Select(_ => Unit.Default).
                           InvokeCommand(this, x => x.ViewModel!.ClothesFilterCommand).
                           DisposeWith(disposable);
            });
        }
    }
}