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
                this.Bind(ViewModel, x => x.Login, x => x.LoginEntry.Text).
                     DisposeWith(disposable);

                this.Bind(ViewModel, x => x.LoginValid, x => x.LoginEntry.IsValid).
                     DisposeWith(disposable);

                this.Bind(ViewModel, x => x.Password, x => x.PasswordEntry.Text).
                     DisposeWith(disposable);

                this.Bind(ViewModel, x => x.PasswordValid, x => x.PasswordEntry.IsValid).
                     DisposeWith(disposable);

                this.Bind(ViewModel, x => x.PasswordConfirm, x => x.PasswordConfirmEntry.Text).
                     DisposeWith(disposable);

                this.Bind(ViewModel, x => x.PasswordConfirmValid, x => x.PasswordConfirmEntry.IsValid).
                     DisposeWith(disposable);

                this.WhenAnyObservable(x => x.ViewModel!.RegisterAuthorizeCommand).
                     WhereNotNull().
                     Select(result => result.HasErrorType(AuthorizeErrorType.Email)).
                     BindTo(this, x => x.LoginEntry.HasError).
                     DisposeWith(disposable);

                this.WhenAnyObservable(x => x.ViewModel!.RegisterAuthorizeCommand).
                     WhereNotNull().
                     Select(result => result.HasErrorType(AuthorizeErrorType.Password)).
                     BindTo(this, x => x.PasswordEntry.HasError).
                     DisposeWith(disposable);
            });
        }
    }
}