using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Implementations.Identities;
using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace BoutiqueXamarin.ViewModels.Authorizes.AuthorizeViewModelItems
{
    /// <summary>
    /// Параметры авторизации и их корректность
    /// </summary>
    public class AuthorizeValidation
    {
        public AuthorizeValidation(string email, bool emailValid, string password, bool passwordValid)
        {
            Email = email;
            EmailValid = emailValid;
            Password = password;
            PasswordValid = passwordValid;
        }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Правильность имени пользователя
        /// </summary>
        public bool EmailValid { get; }

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
            new AuthorizeDomain(Email, Password);
    }
}