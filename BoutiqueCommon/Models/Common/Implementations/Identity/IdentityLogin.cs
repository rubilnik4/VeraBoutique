using BoutiqueCommon.Models.Common.Interfaces.Identity;

namespace BoutiqueCommon.Models.Common.Implementations.Identity
{
    /// <summary>
    /// Имя пользователя и пароль
    /// </summary>
    public class IdentityLogin : IIdentityLogin
    {
        public IdentityLogin(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; }
    }
}