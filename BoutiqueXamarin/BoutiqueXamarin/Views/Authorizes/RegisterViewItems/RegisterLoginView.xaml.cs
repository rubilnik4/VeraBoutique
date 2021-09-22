using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using ResultFunctional.Models.Enums;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.Authorizes.RegisterViewItems
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterLoginView : RegisterLoginBase
    {
        public RegisterLoginView()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel, x => x.Login, x => x.EmailEntry.Text).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.EmailEntry.IsValid).
                     BindTo(this, x => x.ViewModel!.LoginValid).
                     DisposeWith(disposable);

                this.Bind(ViewModel, x => x.Password, x => x.PasswordEntry.Text).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.PasswordEntry.IsValid).
                     BindTo(this, x => x.ViewModel!.PasswordValid).
                     DisposeWith(disposable);

                this.Bind(ViewModel, x => x.PasswordConfirm, x => x.PasswordConfirmEntry.Text).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.PasswordConfirmEntry.HasError).
                     Select(hasError => !hasError).
                     BindTo(this, x => x.ViewModel!.PasswordConfirmValid).
                     DisposeWith(disposable);

                this.WhenAnyObservable(x => x.ViewModel!.RegisterLoginCommand).
                     WhereNotNull().
                     Select(result => result.HasErrorType(AuthorizeErrorType.Email)).
                     BindTo(this, x => x.EmailEntry.HasError).
                     DisposeWith(disposable);

                this.WhenAnyObservable(x => x.ViewModel!.RegisterLoginCommand).
                     WhereNotNull().
                     Select(result => result.HasErrorType(AuthorizeErrorType.Password)).
                     BindTo(this, x => x.PasswordEntry.HasError).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.PasswordEntry.Text, x => x.PasswordConfirmEntry.Text,
                                  (password, passwordConfirm) => (Password: password, PasswordConfirm: passwordConfirm)).
                     Select(passwords => passwords.Password != passwords.PasswordConfirm).
                     BindTo(this, x => x.PasswordConfirmEntry.HasError).
                     DisposeWith(disposable);
            });
        }
    }
}