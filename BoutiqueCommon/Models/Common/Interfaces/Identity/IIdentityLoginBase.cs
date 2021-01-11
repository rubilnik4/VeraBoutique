using BoutiqueCommon.Models.Common.Interfaces.Base;

namespace BoutiqueCommon.Models.Common.Interfaces.Identity
{
    /// <summary>
    /// Имя пользователя и пароль
    /// </summary>
    public interface IIdentityLoginBase : IModel<string>
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Пароль
        /// </summary>
        string Password { get; }
    }
}