using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation;
using BoutiqueXamarin.Models.Implementations.Navigation.Authorize;
using BoutiqueXamarin.ViewModels.Authorizes.AuthorizeViewModelItems;
using BoutiqueXamarin.ViewModels.Authorizes.RegisterViewModelItems;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarinCommon.Infrastructure.Implementations.Authorize;
using BoutiqueXamarinCommon.Infrastructure.Interfaces.Authorize;
using Prism.Navigation;
using ReactiveUI;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueXamarin.ViewModels.Authorizes
{
    /// <summary>
    /// Модель регистрации
    /// </summary>
    public class RegisterViewModel : NavigationViewModel<RegisterNavigationOptions>
    {
        public RegisterViewModel(IUserRestService userRestService, ILoginService loginService, 
                                 IProfileNavigationService profileNavigationService)
            : base(profileNavigationService)
        {
            RegisterLoginViewModel = new RegisterLoginViewModel();
            RegisterPersonalViewModel = new RegisterPersonalViewModel();
            RegisterValidation = new RegisterValidation(RegisterLoginViewModel, RegisterPersonalViewModel);
            RegisterCommand = ReactiveCommand.CreateFromTask<RegisterValidation, IResultError>(
                                async registerValidation => await Register(registerValidation, userRestService, loginService, profileNavigationService));
            _registerErrors = RegisterCommand.ToProperty(this, nameof(RegisterErrors), scheduler: RxApp.MainThreadScheduler);
        }

        /// <summary>
        /// Регистрация. Имя пользователя и пароль
        /// </summary>
        public RegisterLoginViewModel RegisterLoginViewModel { get; }

        /// <summary>
        /// Регистрация. Личная информация
        /// </summary>
        public RegisterPersonalViewModel RegisterPersonalViewModel { get; }

        /// <summary>
        /// Проверка при регистрации
        /// </summary>
        public RegisterValidation RegisterValidation { get; }

        /// <summary>
        /// Команда авторизации
        /// </summary>
        public ReactiveCommand<RegisterValidation, IResultError> RegisterCommand { get; }

        /// <summary>
        /// Ошибки авторизации
        /// </summary>
        private readonly ObservableAsPropertyHelper<IResultError> _registerErrors;

        /// <summary>
        /// Ошибки авторизации
        /// </summary>
        public IResultError RegisterErrors =>
            _registerErrors.Value;

        /// <summary>
        /// Зарегистрировать
        /// </summary>
        private static async Task<IResultError> Register(RegisterValidation registerValidation, IUserRestService userRestService,
                                                                 ILoginService loginService, IProfileNavigationService profileNavigationService) =>
            await registerValidation.ToResultValue().
            ConcatResult(await registerValidation.RegisterLoginViewModel.RegisterLoginCommand.
                         Execute(registerValidation.RegisterLoginViewModel.RegisterLoginValidation).ToTask()).
            ConcatResult(await registerValidation.RegisterPersonalViewModel.RegisterPersonalCommand.
                         Execute(registerValidation.RegisterPersonalViewModel.RegisterPersonalValidation).ToTask()).
            ResultValueBindOkAsync(register => userRestService.Register(register.Register).
                                               ResultValueBindErrorsOkBindAsync(_ => loginService.Login(register.Authorize))).
            ResultValueOkBindAsync(_ => profileNavigationService.ToProfilePage());
    }
}