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
            RegisterCommand = ReactiveCommand.CreateFromTask<Unit, IResultError>(Register);
        }

        /// <summary>
        /// Регистрация. Имя пользователя и пароль
        /// </summary>
        public RegisterLoginViewModel RegisterLoginViewModel { get; }

        /// <summary>
        /// Имя
        /// </summary>
        private string _name = String.Empty;

        /// <summary>
        /// Имя
        /// </summary>
        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        /// <summary>
        /// Корректность имени
        /// </summary>
        public bool NameValid { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        private string _surname = String.Empty;

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname
        {
            get => _surname;
            set => this.RaiseAndSetIfChanged(ref _surname, value);
        }

        /// <summary>
        /// Корректность фамилии
        /// </summary>
        public bool SurnameValid { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        private string _address = String.Empty;

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address
        {
            get => _address;
            set => this.RaiseAndSetIfChanged(ref _address, value);
        }

        /// <summary>
        /// Корректность адреса
        /// </summary>
        public bool AddressValid { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        private string _phone = String.Empty;

        /// <summary>
        /// Адрес
        /// </summary>
        public string Phone
        {
            get => _phone;
            set => this.RaiseAndSetIfChanged(ref _phone, value);
        }

        /// <summary>
        /// Корректность адреса
        /// </summary>
        public bool PhoneValid { get; set; }

        /// <summary>
        /// Команда авторизации
        /// </summary>
        public ReactiveCommand<Unit, IResultError> RegisterCommand { get; }

        /// <summary>
        /// Зарегистрировать
        /// </summary>
        private async Task<IResultError> Register(Unit _) =>
            await new ResultError().
            MapAsync(result => RegisterLoginViewModel.RegisterAuthorizeCommand.
                               Execute(RegisterLoginViewModel.AuthorizeValidation).ToTask().
                               MapTaskAsync(errors => result.ConcatErrors(errors.Errors)));
           // ConcatErrors(ValidateByPassword(authorizeValidation.Password, authorizeValidation.PasswordValid).Errors);
           // ResultValueBindOkAsync(authorize => authorizeRestService.AuthorizeJwt(authorize.AuthorizeDomain)).
          //  ResultValueBindErrorsOkBindAsync(LoginStore.SaveToken);
    }
}