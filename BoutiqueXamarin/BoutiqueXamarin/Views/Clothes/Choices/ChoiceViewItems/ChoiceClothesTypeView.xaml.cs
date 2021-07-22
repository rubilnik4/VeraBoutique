using System.Reactive.Disposables;
using ReactiveUI;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.Clothes.Choices.ChoiceViewItems
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