using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using BoutiqueXamarin.Views.ContentViews.MenuItems;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.Authorizes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : RegisterPageBase
    {
        public RegisterPage()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, x => x.RegisterLoginViewModel, x => x.RegisterLoginView.ViewModel).
                     DisposeWith(disposable);

                this.OneWayBind(ViewModel, x => x.RegisterPersonalViewModel, x => x.RegisterPersonalView.ViewModel).
                     DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.RegisterCommand, x => x.RegisterButton, x => x.RegisterValidation).
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