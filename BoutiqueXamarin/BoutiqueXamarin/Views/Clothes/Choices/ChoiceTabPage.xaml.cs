using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.Clothes.Choices
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChoiceTabPage : ChoiceTabPageBase
    {
        public ChoiceTabPage()
        {
            InitializeComponent();

            this.OneWayBind(ViewModel, x => x.GenderName, x => x.Title);

            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, x => x.GenderName, x => x.LabelMain.Text).
                     DisposeWith(disposable);
            });
        }
    }
}