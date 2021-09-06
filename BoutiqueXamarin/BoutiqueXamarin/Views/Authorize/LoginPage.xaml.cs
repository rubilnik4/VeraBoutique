using System.Linq;
using System.Reactive.Disposables;
using System.Reactive;
using System.Reactive.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueXamarin.Views.ContentViews;
using BoutiqueXamarin.Views.ContentViews.MenuItems;
using ReactiveUI;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.AuthorizeErrors;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;

namespace BoutiqueXamarin.Views.Authorize
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : LoginPageBase
    {
        public LoginPage()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel, x => x.Login, x => x.LoginEntry.Text).
                     DisposeWith(disposable);

                this.Bind(ViewModel, x => x.Password, x => x.PasswordEntry.Text).
                     DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.AuthorizeCommand, x => x.LoginButton, x => x.Authorize).
                     DisposeWith(disposable);

                //this.WhenAnyValue(x => x.ViewModel!.AuthorizeErrors).
                //     WhereNotNull().
                //     Where(result => result.HasErrorType<AuthorizeErrorType>()).
                //     Select(result => result.GetErrorTypes<AuthorizeErrorType>()).
                //     Select(errors => errors.Select(error => error.ErrorType)).
                //     Select(errorTypes => errorTypes.Contains(AuthorizeErrorType.Username)).
                //     Subscribe(hasError => LoginEntry.color = hasError ? Color.Red : Color.Default).
                //     DisposeWith(disposable);
            });
        }

        /// <summary>
        /// Главное окно
        /// </summary>
        protected override BackLeftMenuView BackLeftMenuView =>
            this.BackLeftMenu;
    }
}