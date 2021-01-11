using System.Collections.Generic;
using BoutiqueDTO.Models.Implementations.Identity;
using Microsoft.AspNetCore.Identity;

namespace BoutiqueMVCXUnit.Data.Controllers.Implementations
{
    /// <summary>
    /// Тестовые данные для авторизации
    /// </summary>
    public static class LoginData
    {
        /// <summary>
        /// Имя пользователя и пароль
        /// </summary>
        public static IdentityLoginBaseTransfer IdentityLoginBase =>
          new IdentityLoginBaseTransfer(UserName, "password");

        /// <summary>
        /// Тестовые пользователи
        /// </summary>
        public static IReadOnlyCollection<IdentityUser> Users =>
            new List<IdentityUser>
            {
                new IdentityUser(UserName)
            };

        /// <summary>
        /// Тестовое имя пользователя
        /// </summary>
        public static string UserName => "userName";

        /// <summary>
        /// Тестовые роли
        /// </summary>
        public static IList<string> Roles =>
            new List<string> { "Admin" };
    }
}