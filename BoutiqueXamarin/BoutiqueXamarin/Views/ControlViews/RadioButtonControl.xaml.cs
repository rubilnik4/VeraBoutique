using BoutiqueXamarin.Controls;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.ControlViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RadioButtonControl : StackRadioButton
    {
        public RadioButtonControl()
        {
            InitializeComponent();
        }
    }
}