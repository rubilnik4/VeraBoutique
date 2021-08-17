using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.ContentViews.MenuItems
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BackLeftMenuView : BackLeftMenuBase
    {
        public BackLeftMenuView()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.BindCommand(ViewModel, x => x.BackNavigateCommand, x => x.BackButton);
            });
        }
    }
}