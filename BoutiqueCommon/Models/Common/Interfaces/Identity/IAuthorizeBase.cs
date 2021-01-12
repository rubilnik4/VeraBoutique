using BoutiqueCommon.Models.Common.Interfaces.Base;

namespace BoutiqueCommon.Models.Common.Interfaces.Identity
{
    /// <summary>
    /// Имя пользователя и пароль
    /// </summary>
    public interface IAuthorizeBase : IModel<(string, string)>
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