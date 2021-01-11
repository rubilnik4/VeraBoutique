using BoutiqueCommon.Models.Common.Interfaces.Identity;

namespace BoutiqueCommon.Models.Common.Implementations.Identity
{
    /// <summary>
    /// Имя пользователя и пароль
    /// </summary>
    public class IdentityLoginBase: IIdentityLoginBase
    {
        public IdentityLoginBase(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id => UserName;

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