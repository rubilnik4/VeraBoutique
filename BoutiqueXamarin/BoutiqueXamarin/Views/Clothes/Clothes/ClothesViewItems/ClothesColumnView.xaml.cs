using System.Reactive.Disposables;
using ReactiveUI;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.Clothes.Clothes.ClothesViewItems
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClothesColumnView : ClothesColumnBase
    {
        public ClothesColumnView()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, x => x.ClothesViewModelItemLeft, x => x.ItemLeft.ViewModel).
                     DisposeWith(disposable);
                this.OneWayBind(ViewModel, x => x.ClothesViewModelItemRight, x => x.ItemRight.ViewModel).
                   DisposeWith(disposable);
            });
        }
    }
}