using System;
using ReactiveUI;

namespace BoutiqueXamarin.ViewModels.Authorizes.RegisterViewModelItems
{
    /// <summary>
    /// Параметры личных данных при регистрации и их корректность
    /// </summary>
    public class RegisterPersonalValidation
    {
        public RegisterPersonalValidation(string name, bool nameValid, string surname, bool surnameValid, 
                                        string address, bool addressValid, string phone, bool phoneValid)
        {
            Name = name;
            NameValid = nameValid;
            Surname = surname;
            SurnameValid = surnameValid;
            Address = address;
            AddressValid = addressValid;
            Phone = phone;
            PhoneValid = phoneValid;
        }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Корректность имени
        /// </summary>
        public bool NameValid { get; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; }

        /// <summary>
        /// Корректность фамилии
        /// </summary>
        public bool SurnameValid { get; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; }

        /// <summary>
        /// Корректность адреса
        /// </summary>
        public bool AddressValid { get; }

        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; }

        /// <summary>
        /// Корректность телефона
        /// </summary>
        public bool PhoneValid { get; }
    }
}