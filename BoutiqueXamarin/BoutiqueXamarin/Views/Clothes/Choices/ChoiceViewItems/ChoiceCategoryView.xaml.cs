using System.Reactive.Disposables;
using ReactiveUI;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.Clothes.Choices.ChoiceViewItems
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChoiceCategoryView : ChoiceCategoryBase
    {
        public ChoiceCategoryView()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, x => x.CategoryName, x => x.CategoryName.Text).
                     DisposeWith(disposable);
            });
        }
    }
}