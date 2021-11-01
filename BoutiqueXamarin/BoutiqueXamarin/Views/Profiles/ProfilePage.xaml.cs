using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using BoutiqueXamarin.Views.ContentViews;
using BoutiqueXamarin.Views.ContentViews.MenuItems;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.Profiles
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ProfilePageBase
    {
        public ProfilePage()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, x => x.Profile, x => x.LoginLabel.Text).
                     DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.LogoutCommand, x => x.LogoutButton).
                     DisposeWith(disposable);
            });
        }

        /// <summary>
        /// Главное окно
        /// </summary>
        protected override BackLeftMenuView BackLeftMenuView =>
            this.BackLeftMenu;
    }
}