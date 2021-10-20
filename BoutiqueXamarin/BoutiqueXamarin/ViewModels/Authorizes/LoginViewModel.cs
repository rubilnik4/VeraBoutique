using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Authorizes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Profiles;
using BoutiqueXamarin.Models.Implementations.Navigation.Authorize;
using BoutiqueXamarin.ViewModels.Authorizes.AuthorizeViewModelItems;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarinCommon.Infrastructure.Implementations.Authorize;
using BoutiqueXamarinCommon.Infrastructure.Interfaces.Authorize;
using Prism.Navigation;
using ReactiveUI;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueXamarin.ViewModels.Authorizes
{
    /// <summary>
    /// Модель авторизации
    /// </summary>
    public class LoginViewModel : NavigationBaseViewModel<LoginNavigationParameters, ILoginNavigationService>
    {
        public LoginViewModel(ILoginNavigationService loginNavigationService, ILoginService loginService,
                              IRegisterNavigationService registerNavigationService, IProfileNavigationService profileNavigationService)
          : base(loginNavigationService)
        {
            AuthorizeValidation = this.WhenAnyValue(x => x.Email, x => x.Password).
                                       Select(_ => new AuthorizeValidation(Email, EmailValid, Password, PasswordValid));

            AuthorizeCommand = ReactiveCommand.CreateFromTask<AuthorizeValidation, IResultError>(authorize => JwtAuthorize(authorize, loginService, profileNavigationService));
            _authorizeErrors = AuthorizeCommand.ToProperty(this, nameof(AuthorizeErrors), scheduler: RxApp.MainThreadScheduler);
            RegisterNavigateCommand = ReactiveCommand.CreateFromTask(_ => registerNavigationService.NavigateTo());
        }

        private string _email = String.Empty;

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Email
        {
            get => _email;
            set => this.RaiseAndSetIfChanged(ref _email, value);
        }

        /// <summary>
        /// Корректность имени пользователя
        /// </summary>
        public bool EmailValid { get; set; }

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
        public ReactiveCommand<Unit, INavigationResult> RegisterNavigateCommand { get; }

        /// <summary>
        /// Авторизоваться через токен JWT
        /// </summary>
        private static async Task<IResultError> JwtAuthorize(AuthorizeValidation authorizeValidation, ILoginService loginService,
                                                             IProfileNavigationService profileNavigationService) =>
            await authorizeValidation.ToResultValue().
            ConcatErrors(AuthorizeError.GetResult(authorizeValidation.EmailValid, AuthorizeErrorType.Email, "Почта указана некорректно")).
            ConcatErrors(AuthorizeError.GetResult(authorizeValidation.PasswordValid, AuthorizeErrorType.Password, "Пароль указан некорректно")).
            ResultValueBindErrorsOkAsync(authorize => loginService.Login(authorize.AuthorizeDomain)).
            ResultValueOkBindAsync(_ => profileNavigationService.NavigateTo());
    }
}