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

                this.BindCommand(ViewModel, x => x.RegisterCommand, x => x.RegisterButton, x => x.RegisterLoginViewModel).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.NameEntry.IsValid).
                     BindTo(this, x => x.ViewModel!.NameValid).
                     DisposeWith(disposable);

                this.Bind(ViewModel, x => x.NameValid, x => x.NameEntry.IsValid).
                     DisposeWith(disposable);

                this.Bind(ViewModel, x => x.Surname, x => x.SurnameEntry.Text).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.SurnameEntry.IsValid).
                     BindTo(this, x => x.ViewModel!.SurnameValid).
                     DisposeWith(disposable);

                this.Bind(ViewModel, x => x.Address, x => x.AddressEntry.Text).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.AddressEntry.IsValid).
                     BindTo(this, x => x.ViewModel!.AddressValid).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.PhoneEntry.Text).
                     Select(phone => PhoneCodeEntry.Text + phone).
                     BindTo(this, x => x.ViewModel!.Phone).
                     DisposeWith(disposable);

                this.Bind(ViewModel, x => x.PhoneValid, x => x.PhoneEntry.IsValid).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.PhoneEntry.IsValid).
                     BindTo(this, x => x.ViewModel!.PhoneValid).
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