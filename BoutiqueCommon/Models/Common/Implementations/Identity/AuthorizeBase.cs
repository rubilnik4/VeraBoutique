using System;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Identity;

namespace BoutiqueCommon.Models.Common.Implementations.Identity
{
    /// <summary>
    /// Имя пользователя и пароль
    /// </summary>
    public abstract class AuthorizeBase : IAuthorizeBase
    {
        protected AuthorizeBase(string login, string password)
        {
            Login = login;
            Password = password;
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
        /// Пароль
        /// </summary>
        public string Password { get; }
    }
}