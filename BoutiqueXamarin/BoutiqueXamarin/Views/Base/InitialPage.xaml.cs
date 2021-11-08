using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.Base
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InitialPage : InitialPageBase
    {
        public InitialPage()
        {
            InitializeComponent();
        }
    }
}