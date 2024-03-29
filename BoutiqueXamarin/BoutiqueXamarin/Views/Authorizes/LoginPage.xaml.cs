﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using BoutiqueXamarin.Views.ContentViews.MenuItems;
using ReactiveUI;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.AuthorizeErrors;
using ResultFunctional.Models.Interfaces.Errors.RestErrors;
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
                this.Bind(ViewModel, x => x.Email, x => x.EmailEntry.Text).
                     DisposeWith(disposable);

                this.Bind(ViewModel, x=> x.EmailValid, x => x.EmailEntry.IsValid).
                     DisposeWith(disposable);

                this.Bind(ViewModel, x => x.Password, x => x.PasswordEntry.Text).
                     DisposeWith(disposable);

                this.Bind(ViewModel, x => x.PasswordValid, x => x.PasswordEntry.IsValid).
                     DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.AuthorizeCommand, x => x.LoginButton, ViewModel!.AuthorizeValidation).
                     DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.RegisterNavigateCommand, x => x.RegisterButton).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.ViewModel!.AuthorizeErrors).
                     WhereNotNull().
                     Select(result => result.HasErrorType(AuthorizeErrorType.Email)).
                     BindTo(this, x => x.EmailEntry.HasError).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.ViewModel!.AuthorizeErrors).
                     WhereNotNull().
                     Select(result => result.HasErrorType(AuthorizeErrorType.Password)).
                     BindTo(this, x => x.PasswordEntry.HasError).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.ViewModel!.AuthorizeErrors).
                     WhereNotNull().
                     Where(result => result.HasError<IRestErrorResult>()).
                     Select(result => result.HasErrors).
                     BindTo(this, x => x.AuthorizeErrorLabel.IsVisible).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.EmailEntry.Text, x => x.PasswordEntry.Text).
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
    }
}