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
    public partial class FilterSizeItemView : FilterSizeItemBase
    {
        public FilterSizeItemView()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, x => x.SizeName, x => x.SizeName.Text).
                     DisposeWith(disposable);
            });
        }
    }
}