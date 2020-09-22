using System.Collections.Generic;
using BoutiqueDTO.Models.Implementations.Identity;
using Functional.Models.Interfaces.Result;
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
        public static IdentityLoginTransfer IdentityLogin =>
          new IdentityLoginTransfer("userName", "password");

        /// <summary>
        /// Тестовые пользователи
        /// </summary>
        public static IReadOnlyCollection<IdentityUser> Users =>
            new List<IdentityUser>
            {
                new IdentityUser("UserName")
            };
    }
}