using System.ComponentModel.DataAnnotations;
using BoutiqueCommon.Models.Common.Interfaces.Identity;
using static BoutiqueCommon.Models.Common.Implementations.Identity.IdentitySettings;

namespace BoutiqueDTO.Models.Implementations.Identity
{
    /// <summary>
    /// 
    /// </summary>
    public class IdentityLoginTransfer: IIdentityLogin
    {
        public IdentityLoginTransfer()
        { }

        public IdentityLoginTransfer(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required]
        [MinLength(USERNAME_MINLENGTH)]
        public string UserName { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required]
        [MinLength(PASSWORD_MINLENGTH)]
        public string Password { get; set; }
    }
}