using System;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;

namespace BoutiqueCommon.Models.Common.Interfaces.Identity
{
    /// <summary>
    /// Имя пользователя и пароль
    /// </summary>
    public interface IAuthorizeBase : IModel<string>
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        string Login { get; }

        /// <summary>
        /// Пароль
        /// </summary>
        string Password { get; }
    }
}