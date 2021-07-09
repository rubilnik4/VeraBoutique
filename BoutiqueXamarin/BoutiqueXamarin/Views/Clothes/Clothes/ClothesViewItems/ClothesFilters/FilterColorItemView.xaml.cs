using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.Clothes.Clothes.ClothesViewItems.ClothesFilters
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterColorItemView : FilterColorItemBase
    {
        public FilterColorItemView()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel, x => x.IsChecked, x => x.ColorCheck.IsChecked).
                    DisposeWith(disposable);

                this.OneWayBind(ViewModel, x => x.ColorName, x => x.ColorName.Text).
                     DisposeWith(disposable);

                this.ColorCheck.Events().
                     CheckedChanged.
                     Select(_ => Unit.Default).
                     InvokeCommand(this, x => x.ViewModel!.ClothesFilterCommand).
                     DisposeWith(disposable);
            });
        }
    }
}