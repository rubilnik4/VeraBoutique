using System;
using System.Reactive.Linq;
using BoutiqueXamarin.Infrastructure.Implementations.Validation;
using BoutiqueXamarin.ViewModels.Authorizes.AuthorizeViewModelItems;
using BoutiqueXamarin.ViewModels.Base;
using ReactiveUI;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueXamarin.ViewModels.Authorizes.RegisterViewModelItems
{
    /// <summary>
    /// Регистрация. Имя пользователя и пароль
    /// </summary>
    public class RegisterLoginViewModel : BaseViewModel
    {
        public RegisterLoginViewModel()
        {
            _authorizeValidation = this.WhenAnyValue(x => x.Login, x => x.Password, (login, password) => (Login: login, Password: password)).
                                        Select(authorize => new AuthorizeValidation(authorize.Login, LoginValid, authorize.Password, PasswordValid)).
                                        ToProperty(this, nameof(AuthorizeValidation));
            RegisterAuthorizeCommand = ReactiveCommand.Create<AuthorizeValidation, IResultError>(GetRegisterLoginErrors);
        }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        private string _login = String.Empty;

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Login
        {
            get => _login;
            set => this.RaiseAndSetIfChanged(ref _login, value);
        }

        /// <summary>
        /// Корректность имени пользователя
        /// </summary>
        public bool LoginValid { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        private string _password = String.Empty;

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        /// <summary>
        /// Корректность пароля
        /// </summary>
        public bool PasswordValid { get; set; }

        /// <summary>
        /// Подтверждение пароля
        /// </summary>
        public string PasswordConfirm
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        /// <summary>
        /// Корректность подтверждения пароля
        /// </summary>
        public bool PasswordConfirmValid { get; set; }

        /// <summary>
        /// Параметры авторизации
        /// </summary>
        private readonly ObservableAsPropertyHelper<AuthorizeValidation> _authorizeValidation;

        /// <summary>
        /// Параметры авторизации
        /// </summary>
        public AuthorizeValidation AuthorizeValidation =>
            _authorizeValidation.Value;

        /// <summary>
        /// Команда проверки имени пользователя и пароля
        /// </summary>
        public ReactiveCommand<AuthorizeValidation, IResultError> RegisterAuthorizeCommand { get; }

        /// <summary>
        /// Получить ошибки проверки логина и пароля
        /// </summary>
        public static IResultError GetRegisterLoginErrors(AuthorizeValidation authorizeValidation) =>
            authorizeValidation.ToResultValue().
            ConcatErrors(AuthorizeError.GetEmailError(authorizeValidation.Login, authorizeValidation.LoginValid).Errors).
            ConcatErrors(AuthorizeError.GetPasswordError(authorizeValidation.Password, authorizeValidation.PasswordValid).Errors);
    }
}