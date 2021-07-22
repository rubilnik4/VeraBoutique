using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.Clothes.ClothesDetails.ClothesDetailsViewItems
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClothesDetailImageView : ClothesDetailImageBase
    {
        public ClothesDetailImageView()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, x => x.Image, x => x.ClothesImage.Source).
                     DisposeWith(disposable);
            });
        }
    }
}