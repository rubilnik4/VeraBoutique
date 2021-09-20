using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Authorizes;
using BoutiqueXamarin.Models.Implementations.Navigation.Authorize;
using BoutiqueXamarin.ViewModels.Authorizes.AuthorizeViewModelItems;
using BoutiqueXamarin.ViewModels.Authorizes.RegisterViewModelItems;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarinCommon.Infrastructure.Implementations.Authorize;
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
    public class RegisterViewModel : NavigationBaseViewModel<RegisterNavigationParameters, IRegisterNavigationService>
    {
        public RegisterViewModel(IRegisterNavigationService registerNavigationService)
            : base(registerNavigationService)
        {
            RegisterLoginViewModel = new RegisterLoginViewModel();
            RegisterPersonalViewModel = new RegisterPersonalViewModel();
            RegisterCommand = ReactiveCommand.CreateFromTask<RegisterLoginViewModel, IResultError>(Register);
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
        /// Команда авторизации
        /// </summary>
        public ReactiveCommand<RegisterLoginViewModel, IResultError> RegisterCommand { get; }

        /// <summary>
        /// Зарегистрировать
        /// </summary>
        private static async Task<IResultError> Register(RegisterLoginViewModel registerLoginViewModel) =>
            await new ResultError().
            MapAsync(result => registerLoginViewModel.RegisterLoginCommand.
                               Execute(registerLoginViewModel.RegisterLoginValidation).ToTask().
                               MapTaskAsync(errors => result.ConcatErrors(errors.Errors)));
           // ConcatErrors(ValidateByPassword(authorizeValidation.Password, authorizeValidation.PasswordValid).Errors);
           // ResultValueBindOkAsync(authorize => authorizeRestService.AuthorizeJwt(authorize.AuthorizeDomain)).
          //  ResultValueBindErrorsOkBindAsync(LoginStore.SaveToken);
    }
}