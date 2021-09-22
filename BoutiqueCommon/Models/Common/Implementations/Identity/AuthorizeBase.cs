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
        protected AuthorizeBase(string email, string password)
        {
            Email = email;
            Password = password;
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
        public string Email {get; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; }

        #region IEquatable
        public override bool Equals(object? obj) =>
          obj is IAuthorizeBase authorize && Equals(authorize);

        public bool Equals(IAuthorizeBase? other) =>
            other?.Id == Id;

        public override int GetHashCode() =>
            HashCode.Combine(Email, Password);
        #endregion
    }
}