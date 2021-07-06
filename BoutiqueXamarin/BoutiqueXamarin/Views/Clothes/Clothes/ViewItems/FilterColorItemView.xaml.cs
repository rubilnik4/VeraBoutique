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