using System;
using System.Reactive.Linq;
using BoutiqueXamarin.Infrastructure.Implementations.Validation;
using BoutiqueXamarin.ViewModels.Authorizes.AuthorizeViewModelItems;
using BoutiqueXamarin.ViewModels.Base;
using ReactiveUI;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
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
            _registerLoginValidation = this.WhenAnyValue(x => x.LoginValid, x => x.PasswordValid, x => x.PasswordConfirmValid,
                                                        (loginValid, passwordValid, passwordConfirmValid) => (loginValid, passwordValid, passwordConfirmValid)).
                                        Select(registerLogin => new RegisterLoginValidation(Login, registerLogin.loginValid, Password, registerLogin.passwordValid,
                                                                                            PasswordConfirm, registerLogin.passwordConfirmValid)).
                                        ToProperty(this, nameof(RegisterLoginValidation));
            RegisterLoginCommand = ReactiveCommand.Create<RegisterLoginValidation, IResultError>(GetRegisterLoginErrors);
        }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Login { get; set; } = String.Empty;

        /// <summary>
        /// Имя пользователя
        /// </summary>
        private bool _loginValid;

        /// <summary>
        /// Корректность имени пользователя
        /// </summary>
        public bool LoginValid
        {
            get => _loginValid;
            set => this.RaiseAndSetIfChanged(ref _loginValid, value);
        }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; } = String.Empty;

        /// <summary>
        /// Пароль
        /// </summary>
        private bool _passwordValid;

        /// <summary>
        /// Корректность пароля
        /// </summary>
        public bool PasswordValid
        {
            get => _passwordValid;
            set => this.RaiseAndSetIfChanged(ref _passwordValid, value);
        }

        /// <summary>
        /// Подтверждение пароля
        /// </summary>
        public string PasswordConfirm { get; set; } = String.Empty;

        /// <summary>
        /// Корректность подтверждения пароля
        /// </summary>
        private bool _passwordConfirmValid;

        /// <summary>
        /// Корректность подтверждения пароля
        /// </summary>
        public bool PasswordConfirmValid
        {
            get => _passwordConfirmValid;
            set => this.RaiseAndSetIfChanged(ref _passwordConfirmValid, value);
        }

        /// <summary>
        /// Параметры регистрации и их корректность
        /// </summary>
        private readonly ObservableAsPropertyHelper<RegisterLoginValidation> _registerLoginValidation;

        /// <summary>
        /// Параметры регистрации и их корректность
        /// </summary>
        public RegisterLoginValidation RegisterLoginValidation =>
            _registerLoginValidation.Value;

        /// <summary>
        /// Команда проверки имени пользователя и пароля
        /// </summary>
        public ReactiveCommand<RegisterLoginValidation, IResultError> RegisterLoginCommand { get; }

        /// <summary>
        /// Получить ошибки проверки логина и пароля
        /// </summary>
        public static IResultError GetRegisterLoginErrors(RegisterLoginValidation registerLoginValidation) =>
            registerLoginValidation.ToResultValue().
            ConcatErrors(AuthorizeError.GetResult(registerLoginValidation.LoginValid, AuthorizeErrorType.Email, "Почта указана некорректно")).
            ConcatErrors(AuthorizeError.GetResult(registerLoginValidation.PasswordValid, AuthorizeErrorType.Password, "Пароль указан некорректно")).
            ConcatErrors(AuthorizeError.GetResult(registerLoginValidation.PasswordConfirmValid, AuthorizeErrorType.PasswordConfirm, "Пароли не совпадают"));
    }
}