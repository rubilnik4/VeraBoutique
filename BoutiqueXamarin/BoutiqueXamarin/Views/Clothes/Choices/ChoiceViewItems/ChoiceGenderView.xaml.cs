using System.Reactive.Disposables;
using ReactiveUI;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.Clothes.Choices.ChoiceViewItems
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChoiceGenderView : ChoiceGenderBase
    {
        public ChoiceGenderView()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, x => x.GenderName, x => x.GenderLabel.Text).
                     DisposeWith(disposable);
            });
        }
    }
}