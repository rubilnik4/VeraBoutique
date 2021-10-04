using System;
using BoutiqueCommon.Models.Enums.Identity;

namespace BoutiqueCommon.Models.Common.Interfaces.Identity
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public interface IBoutiqueUser
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Почта
        /// </summary>
        string Email { get; }

        /// <summary>
        /// Роль
        /// </summary>
        IdentityRoleType IdentityRoleType { get; }

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
        string PhoneNumber { get; }
    }
}