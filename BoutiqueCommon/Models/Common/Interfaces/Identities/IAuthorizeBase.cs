using System;
using BoutiqueCommon.Models.Common.Interfaces.Base;

namespace BoutiqueCommon.Models.Common.Interfaces.Identities
{
    /// <summary>
    /// Имя пользователя и пароль
    /// </summary>
    public interface IAuthorizeBase : IModel<string>, IEquatable<IAuthorizeBase>
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
        /// Пароль
        /// </summary>
        string Password { get; }
    }
}