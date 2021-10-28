using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoutiqueXamarin.ViewModels.Base.MenuItems;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.ContentViews.MenuItems
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserRightMenuView : UserRightMenuBase
    {
        public UserRightMenuView()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.BindCommand(ViewModel, x => x.UserNavigateCommand, x => x.UserButton);
            });
        }
    }
}