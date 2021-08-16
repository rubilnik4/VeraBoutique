using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using BoutiqueXamarinCommon.Infrastructure.Implementations.Calculate;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.Clothes.ClothesDetails.ClothesDetailsViewItems
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClothesDetailDescriptionView : ClothesDetailDescriptionBase
    {
        public ClothesDetailDescriptionView()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, x => x.Name, x => x.NameLabel.Text).
                     DisposeWith(disposable);

                this.OneWayBind(ViewModel, x => x.Price, x => x.PriceLabel.Text, ClothesPrices.ConvertingPrice).
                     DisposeWith(disposable);

                this.OneWayBind(ViewModel, x => x.Description, x => x.DescriptionLabel.Text).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.ViewModel!.Sizes).
                     WhereNotNull().
                     Select(items => (IList)items).
                     BindTo(this, x => x.SizePicker.ItemsSource).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.ViewModel!.Colors).
                     WhereNotNull().
                     Select(items => (IList)items).
                     BindTo(this, x => x.ColorPicker.ItemsSource).
                     DisposeWith(disposable);
            });
        }
    }
}