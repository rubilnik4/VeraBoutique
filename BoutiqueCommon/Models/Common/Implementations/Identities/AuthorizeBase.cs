using System;
using BoutiqueCommon.Models.Common.Interfaces.Identities;

namespace BoutiqueCommon.Models.Common.Implementations.Identities
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
            other?.Email == Email &&
            other?.Password == Password;

        public override int GetHashCode() =>
            HashCode.Combine(Email, Password);
        #endregion
    }
}