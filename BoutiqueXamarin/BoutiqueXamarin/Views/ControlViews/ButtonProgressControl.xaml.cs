using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using BoutiqueXamarin.Controls;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.ControlViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ButtonProgressControl : StackLayoutButton
    {
        public ButtonProgressControl()
        {
            InitializeComponent();

            this.WhenAnyValue(x => x.ButtonMain.IsEnabled).
                 Select(isEnabled => !isEnabled).
                 BindTo(this, x => x.StackProgress.IsVisible);
        }
    }
}