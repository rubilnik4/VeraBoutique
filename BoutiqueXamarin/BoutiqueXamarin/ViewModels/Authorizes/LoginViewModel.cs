using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize;
using BoutiqueXamarin.Infrastructure.Implementations.Validation;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Authorizes;
using BoutiqueXamarin.Models.Implementations.Navigation.Authorize;
using BoutiqueXamarin.ViewModels.Authorizes.AuthorizeViewModelItems;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarinCommon.Infrastructure.Implementations.Authorize;
using ReactiveUI;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueXamarin.ViewModels.Authorizes
{
    /// <summary>
    /// Модель авторизации
    /// </summary>
    public class LoginViewModel : NavigationBaseViewModel<LoginNavigationParameters, ILoginNavigationService>
    {
        public LoginViewModel(ILoginNavigationService loginNavigationService, IRegisterNavigationService registerNavigationService,
                              IAuthorizeRestService authorizeRestService)
          : base(loginNavigationService)
        {
            AuthorizeValidation = this.WhenAnyValue(x => x.Login, x => x.Password, (login, password) => (Login: login, Password: password)).
                                       Select(authorize => new AuthorizeValidation(authorize.Login, LoginValid, authorize.Password, PasswordValid));

            AuthorizeCommand = ReactiveCommand.CreateFromTask<AuthorizeValidation, IResultError>(authorize => JwtAuthorize(authorize, authorizeRestService));
            _authorizeErrors = AuthorizeCommand.ToProperty(this, nameof(AuthorizeErrors), scheduler: RxApp.MainThreadScheduler);
            RegisterNavigateCommand = ReactiveCommand.CreateFromTask(_ => registerNavigationService.NavigateTo());
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
        /// Параметры авторизации
        /// </summary>
        public IObservable<AuthorizeValidation> AuthorizeValidation { get; }

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
        public ReactiveCommand<AuthorizeValidation, IResultError> AuthorizeCommand { get; }

        /// <summary>
        /// Переход к странице регистрации
        /// </summary>
        public ReactiveCommand<Unit, Unit> RegisterNavigateCommand { get; }

        /// <summary>
        /// Авторизоваться через токен JWT
        /// </summary>
        private static async Task<IResultError> JwtAuthorize(AuthorizeValidation authorizeValidation, IAuthorizeRestService authorizeRestService) =>
            await authorizeValidation.ToResultValue().
            ConcatErrors(AuthorizeError.GetEmailError(authorizeValidation.Login, authorizeValidation.LoginValid).Errors).
            ConcatErrors(AuthorizeError.GetPasswordError(authorizeValidation.Password, authorizeValidation.PasswordValid).Errors).
            ResultValueBindOkAsync(authorize => authorizeRestService.AuthorizeJwt(authorize.AuthorizeDomain)).
            ResultValueBindErrorsOkBindAsync(LoginStore.SaveToken);

     
    }
}