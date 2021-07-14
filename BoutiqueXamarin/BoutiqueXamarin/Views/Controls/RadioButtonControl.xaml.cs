using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoutiqueXamarin.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.Controls
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