using System;
using BoutiqueCommon.Models.Common.Interfaces.Base;

namespace BoutiqueCommon.Models.Common.Interfaces.Identity
{
    /// <summary>
    /// Регистрация
    /// </summary>
    public interface IRegisterBase : IModel<string>, IEquatable<IRegisterBase>
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        string Login { get; }

        /// <summary>
        /// Почта
        /// </summary>
        string Email { get; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        string Password { get; }

        /// <summary>
        /// Имя
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Фамилия
        /// </summary>
        string Surname { get; }

        /// <summary>
        /// Адрес
        /// </summary>
        string Address { get; }

        /// <summary>
        /// Телефон
        /// </summary>
        string Phone { get; }
    }
}