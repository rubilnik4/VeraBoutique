using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.Clothes.Clothes.ViewItems
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