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
using System.Collections.Generic;

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

                var authorizeErrors = GetAuthorizeErrors();
                authorizeErrors.
                    Select(errorTypes => errorTypes.Contains(AuthorizeErrorType.Username) ||
                                         errorTypes.Contains(AuthorizeErrorType.Email) ||
                                         errorTypes.Contains(AuthorizeErrorType.Phone)).
                    BindTo(this, x => x.LoginEntry.HasError).
                    DisposeWith(disposable);

                authorizeErrors.
                  Select(errorTypes => errorTypes.Contains(AuthorizeErrorType.Password)).
                  BindTo(this, x => x.PasswordEntry.HasError).
                  DisposeWith(disposable);

                authorizeErrors.
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