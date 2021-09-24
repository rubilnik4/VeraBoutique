using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueDAL.Models.Implementations.Identity;
using BoutiqueDTO.Models.Implementations.Identity;
using Microsoft.AspNetCore.Identity;

namespace BoutiqueMVCXUnit.Data.Controllers.Implementations
{
    /// <summary>
    /// Тестовые данные для авторизации
    /// </summary>
    public static class IdentityData
    {
        /// <summary>
        /// Тестовые пользователи
        /// </summary>
        public static IReadOnlyCollection<BoutiqueUser> Users =>
            new List<BoutiqueUser>
            {
                new (AuthorizeData.AuthorizeDomains.First().Email, AuthorizeData.AuthorizeDomains.First().Password, 
                     "Name", "Surname", "Address", "+79224725787")
            };

        /// <summary>
        /// Тестовые роли
        /// </summary>
        public static IList<string> Roles =>
            new List<string> { "Admin" };
    }
}