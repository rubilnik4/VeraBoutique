using System.ComponentModel.DataAnnotations;
using BoutiqueCommon.Models.Common.Interfaces.Identity;

namespace BoutiqueDTO.Models.Implementations.Identity
{
    /// <summary>
    /// 
    /// </summary>
    public class IdentityLoginTransfer: IIdentityLogin
    {
        public IdentityLoginTransfer(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required]
        public string UserName { get; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required]
        public string Password { get; }
    }
}