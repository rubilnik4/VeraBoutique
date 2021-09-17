using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;

namespace BoutiqueXamarin.ViewModels.Authorizes.AuthorizeViewModelItems
{
    /// <summary>
    /// Параметры авторизации и их корректность
    /// </summary>
    public class AuthorizeValidation
    {
        public AuthorizeValidation(string login, bool loginValid, string password, bool passwordValid)
        {
            Login = login;
            LoginValid = loginValid;
            Password = password;
            PasswordValid = passwordValid;
        }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Login { get; }

        /// <summary>
        /// Правильность имени пользователя
        /// </summary>
        public bool LoginValid { get; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Password { get; }

        /// <summary>
        /// Правильность имени пользователя
        /// </summary>
        public bool PasswordValid { get; }

        /// <summary>
        /// Имя пользователя и пароль
        /// </summary>
        public IAuthorizeDomain AuthorizeDomain =>
            new AuthorizeDomain(Login, Password);
    }
}