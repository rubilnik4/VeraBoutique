using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.Clothes.Choices.Tabs.ViewItems
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChoiceClothesTypeView : ChoiceClothesTypeBase
    {
        public ChoiceClothesTypeView()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, x => x.ClothesTypeName, x => x.ClothesTypeName.Text).
                     DisposeWith(disposable);
            });
        }
    }
}