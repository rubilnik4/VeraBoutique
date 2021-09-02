using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Authorize;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Authorize;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarinCommon.Infrastructure.Implementations.Authorize;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using ReactiveUI;

namespace BoutiqueXamarin.ViewModels.Authorize
{
    /// <summary>
    /// Модель авторизации
    /// </summary>
    public class LoginViewModel : NavigationBaseViewModel<LoginNavigationParameters, ILoginNavigationService>
    {
        public LoginViewModel(ILoginNavigationService loginNavigationService, IAuthorizeRestService authorizeRestService)
          : base(loginNavigationService, loginNavigationService)
        {
            _authorize = this.WhenAnyValue(x => x.Login, x => x.Password, (login, password) => (Login: login, Password: password)).
                              Select(x => (IAuthorizeDomain)new AuthorizeDomain(x.Login, x.Password)).
                              ToProperty(this, nameof(Authorize));

            AuthorizeCommand = ReactiveCommand.CreateFromTask<IAuthorizeDomain, IResultError>(
                                   authorize => JwtAuthorize(authorize, authorizeRestService));
            _authorizeErrors = AuthorizeCommand.ToProperty(this, nameof(AuthorizeErrors));
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
        /// Имя пользователя
        /// </summary>
        private string _password = String.Empty;

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        /// <summary>
        /// Параметры авторизации
        /// </summary>
        private readonly ObservableAsPropertyHelper<IAuthorizeDomain> _authorize;

        /// <summary>
        /// Параметры авторизации
        /// </summary>
        public IAuthorizeDomain Authorize =>
            _authorize.Value;

        /// <summary>
        /// Ошибки авторизации
        /// </summary>
        private readonly ObservableAsPropertyHelper<IResultError> _authorizeErrors;

        /// <summary>
        /// Ошибки авторизации
        /// </summary>
        public IResultError AuthorizeErrors =>
            _authorizeErrors.Value;

        /// <summary>
        /// Команда авторизации
        /// </summary>
        public ReactiveCommand<IAuthorizeDomain, IResultError> AuthorizeCommand { get; }

        /// <summary>
        /// Авторизоваться через токен JWT
        /// </summary>
        private static async Task<IResultError> JwtAuthorize(IAuthorizeDomain authorizeDomain, IAuthorizeRestService authorizeRestService) =>
            await authorizeDomain.ToResultValue().
            ConcatErrors(ValidateByLogin(authorizeDomain.UserName).Errors).
            ConcatErrors(ValidateByPassword(authorizeDomain.Password).Errors).
            ResultValueBindOkAsync(authorizeRestService.AuthorizeJwt).
            ResultValueBindErrorsOkBindAsync(LoginStore.SaveToken);

        /// <summary>
        /// Проверка по имени пользователя
        /// </summary>
        private static IResultValue<string> ValidateByLogin(string loginInitial) =>
            loginInitial.ToResultValueWhere(login => !String.IsNullOrWhiteSpace(login),
                                          _ => ErrorResultFactory.AuthorizeError(AuthorizeErrorType.Username, "Не указано имя пользователя"));

        /// <summary>
        /// Проверка по имени пользователя
        /// </summary>
        private static IResultValue<string> ValidateByPassword(string passwordInitial) =>
            passwordInitial.ToResultValueWhere(password => !String.IsNullOrWhiteSpace(password),
                                          _ => ErrorResultFactory.AuthorizeError(AuthorizeErrorType.Password, "Не указан пароль"));
    }
}