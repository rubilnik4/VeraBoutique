using System;
using System.Reactive.Linq;
using BoutiqueXamarin.ViewModels.Base;
using ReactiveUI;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueXamarin.ViewModels.Authorizes.RegisterViewModelItems
{
    /// <summary>
    /// Регистрация. Личная информация
    /// </summary>
    public class RegisterPersonalViewModel : BaseViewModel
    {
        public RegisterPersonalViewModel()
        {
            _registerPersonalValidation = this.WhenAnyValue(x => x.NameValid, x => x.SurnameValid, x => x.AddressValid, x => x.PhoneValid,
                                                            (nameValid, surnameValid, addressValid, phoneValid) => (nameValid, surnameValid, addressValid, phoneValid)).
                                          Select(personal => new RegisterPersonalValidation(Name, personal.nameValid, Surname, personal.surnameValid,
                                                                                            Address, personal.addressValid, Phone, personal.phoneValid)).
                                          ToProperty(this, nameof(RegisterPersonalValidation));
            RegisterPersonalCommand = ReactiveCommand.Create<RegisterPersonalValidation, IResultError>(GetRegisterPersonalErrors);
        }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// Корректность имени
        /// </summary>
        private bool _nameValid;

        /// <summary>
        /// Корректность имени
        /// </summary>
        public bool NameValid
        {
            get => _nameValid;
            set => this.RaiseAndSetIfChanged(ref _nameValid, value);
        }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; } = String.Empty;

        /// <summary>
        /// Корректность фамилии
        /// </summary>
        private bool _surnameValid;

        /// <summary>
        /// Корректность фамилии
        /// </summary>
        public bool SurnameValid
        {
            get => _surnameValid;
            set => this.RaiseAndSetIfChanged(ref _surnameValid, value);
        }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; } = String.Empty;

        /// <summary>
        /// Корректность адреса
        /// </summary>
        private bool _addressValid;

        /// <summary>
        /// Корректность адреса
        /// </summary>
        public bool AddressValid
        {
            get => _addressValid;
            set => this.RaiseAndSetIfChanged(ref _addressValid, value);
        }

        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; } = String.Empty;

        /// <summary>
        /// Корректность телефона
        /// </summary>
        private bool _phoneValid;

        /// <summary>
        /// Корректность телефона
        /// </summary>
        public bool PhoneValid
        {
            get => _phoneValid;
            set => this.RaiseAndSetIfChanged(ref _phoneValid, value);
        }

        /// <summary>
        /// Параметры личных данных при регистрации и их корректность
        /// </summary>
        private readonly ObservableAsPropertyHelper<RegisterPersonalValidation> _registerPersonalValidation;

        /// <summary>
        /// Параметры личных данных при регистрации и их корректность
        /// </summary>
        public RegisterPersonalValidation RegisterPersonalValidation =>
            _registerPersonalValidation.Value;

        /// <summary>
        /// Команда проверки имени пользователя и пароля
        /// </summary>
        public ReactiveCommand<RegisterPersonalValidation, IResultError> RegisterPersonalCommand { get; }

        /// <summary>
        /// Получить ошибки проверки логина и пароля
        /// </summary>
        public static IResultError GetRegisterPersonalErrors(RegisterPersonalValidation personalValidation) =>
            personalValidation.ToResultValue().
            ConcatErrors(RegisterPersonalError.GetResult(personalValidation.Name, personalValidation.NameValid, "Имя указано некорректно")).
            ConcatErrors(RegisterPersonalError.GetResult(personalValidation.Surname, personalValidation.SurnameValid, "Фамилия указана некорректно")).
            ConcatErrors(RegisterPersonalError.GetResult(personalValidation.Address, personalValidation.AddressValid, "Адрес указан некорректно")).
            ConcatErrors(RegisterPersonalError.GetResult(personalValidation.Phone, personalValidation.PhoneValid, "Телефон указан некорректно"));
    }
}