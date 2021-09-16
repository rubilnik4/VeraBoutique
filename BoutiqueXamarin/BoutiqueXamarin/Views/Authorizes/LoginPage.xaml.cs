using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using BoutiqueXamarin.Views.ContentViews.MenuItems;
using ReactiveUI;
using ResultFunctional.Models.Enums;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.Authorizes
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

                this.Bind(ViewModel, x => x.LoginValidationCommand, x => x.LoginEntry.ValidateCommand).
                     DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.RegisterNavigateCommand, x => x.RegisterButton).
                     DisposeWith(disposable);

                this.GetAuthorizeErrors().
                     Select(errorTypes => errorTypes.Contains(AuthorizeErrorType.Token)).
                     BindTo(this, x => x.AuthorizeErrorLabel.IsVisible).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.LoginEntry.Text, x => x.PasswordEntry.Text).
                     Where(_ => AuthorizeErrorLabel.IsVisible).
                     Subscribe(_ => AuthorizeErrorLabel.IsVisible = false).
                     DisposeWith(disposable);
            });
        }

        /// <summary>
        /// Главное окно
        /// </summary>
        protected override BackLeftMenuView BackLeftMenuView =>
            this.BackLeftMenu;

        /// <summary>
        /// Получить ошибки авторизации
        /// </summary>
        private IObservable<IReadOnlyCollection<AuthorizeErrorType>> GetAuthorizeErrors() =>
            this.WhenAnyValue(x => x.ViewModel!.AuthorizeErrors).
                 WhereNotNull().
                 Select(result => result.GetErrorTypes<AuthorizeErrorType>().
                                         Select(error => error.ErrorType).
                                         ToList());
    }
}