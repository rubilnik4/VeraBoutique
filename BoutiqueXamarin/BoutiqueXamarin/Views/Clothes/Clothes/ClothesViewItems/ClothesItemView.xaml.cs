using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.Clothes.Clothes.ClothesViewItems
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClothesItemView : ClothesItemBase
    {
        public ClothesItemView()
        {
            InitializeComponent();

            var tapGestureRecognizer = new TapGestureRecognizer();
            ClothesItem.GestureRecognizers.Add(tapGestureRecognizer);

            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, x => x.Image, x => x.ClothesImage.Source).
                     DisposeWith(disposable);

                this.OneWayBind(ViewModel, x => x.Name, x => x.ClothesName.Text).
                     DisposeWith(disposable);

                this.OneWayBind(ViewModel, x => x.Price, x => x.ClothesPrice.Text, price => price.ToString("F0") + " Руб.").
                     DisposeWith(disposable);

                tapGestureRecognizer.
                    Events().Tapped.
                    Select(_ => Unit.Default).
                    InvokeCommand(this, x => x.ViewModel!.ClothesDetailCommand).
                    DisposeWith(disposable);
            });
        }
    }
}