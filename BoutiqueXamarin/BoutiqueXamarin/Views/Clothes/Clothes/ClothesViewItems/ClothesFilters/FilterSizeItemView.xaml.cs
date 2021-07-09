using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.Clothes.Clothes.ClothesViewItems.ClothesFilters
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterSizeItemView : FilterSizeItemBase
    {
        public FilterSizeItemView()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel, x => x.IsChecked, x => x.SizeCheck.IsChecked).
                     DisposeWith(disposable);

                this.OneWayBind(ViewModel, x => x.SizeName, x => x.SizeName.Text).
                     DisposeWith(disposable);

                this.SizeCheck.Events().
                     CheckedChanged.
                     Select(_ => Unit.Default).
                     InvokeCommand(this, x => x.ViewModel!.ClothesFilterCommand).
                     DisposeWith(disposable);
            });
        }
    }
}