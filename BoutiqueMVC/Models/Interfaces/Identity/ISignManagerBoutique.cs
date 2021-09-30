using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BoutiqueMVC.Models.Interfaces.Identity
{
    /// <summary>
    /// Менеджер аутентификации
    /// </summary>
    public interface ISignInManagerBoutique
    {
        /// <summary>
        /// Проверить пароль для авторизации
        /// </summary>
        Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockOutOnFailure);
    }
}