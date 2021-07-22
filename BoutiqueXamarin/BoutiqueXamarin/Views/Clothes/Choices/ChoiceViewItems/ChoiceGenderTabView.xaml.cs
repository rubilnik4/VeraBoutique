using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.Clothes.Choices.ChoiceViewItems
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChoiceGenderTabView : ChoiceGenderTabBase
    {
        public ChoiceGenderTabView()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, x => x.ChoiceBaseViewModelItems, x => x.CategoryListView.ItemsSource).
                     DisposeWith(disposable);

                this.CategoryListView.Events().ItemTapped.
                     Select(_ => CategoryListView.SelectedItem).
                     InvokeCommand(this, x => x.ViewModel!.ChoiceBaseTapCommand).
                     DisposeWith(disposable);
            });
        }
    }
}