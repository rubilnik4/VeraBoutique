using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using ReactiveUI;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.Clothes.ClothesDetails
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClothesDetailPage : ClothesDetailPageBase
    {
        public ClothesDetailPage()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.BindCommand(ViewModel, x => x.NavigateBackCommand, x => x.BackButton);
            });
        }
    }
}