using System;
using BoutiqueCommon.Models.Common.Interfaces.Identity;

namespace BoutiqueCommon.Models.Common.Implementations.Identity
{
    /// <summary>
    /// Регистрация
    /// </summary>
    public class RegisterBase: IRegisterBase
    {
        public RegisterBase(string login, string password, string name, string surname, string address, string phone)
        {
            Login = login;
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
        public string Login { get; }

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
    }
}