using System;
using BoutiqueCommon.Models.Common.Interfaces.Identity;

namespace BoutiqueCommon.Models.Common.Implementations.Identity
{
    /// <summary>
    /// Регистрация
    /// </summary>
    public class RegisterBase: IRegisterBase
    {
        public RegisterBase(string email, string password, string name, string surname, string address, string phone)
        {
            Email = email;
            Password = password;
            Name = name;
            Surname = surname;
            Address = address;
            Phone = phone;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id => Login;

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Login =>
            Email;

        /// <summary>
        /// Почта
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Password { get; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; }

        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; }

        #region IEquatable
        public override bool Equals(object? obj) =>
          obj is IRegisterBase register && Equals(register);

        public bool Equals(IRegisterBase? other) =>
            other?.Id == Id;

        public override int GetHashCode() =>
            HashCode.Combine(Email, Password);
        #endregion
    }
}