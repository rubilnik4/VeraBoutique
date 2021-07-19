using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.Clothes.Choices.Tabs.ViewItems
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