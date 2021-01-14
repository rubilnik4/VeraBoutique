using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;

namespace BoutiqueCommonXUnit.Data.Authorize
{
    /// <summary>
    /// Данные логина и пароля
    /// </summary>
    public static class AuthorizeData
    {
        /// <summary>
        /// Получить категории одежды
        /// </summary>
        public static IReadOnlyCollection<IAuthorizeDomain> AuthorizeDomain =>
            new List<IAuthorizeDomain>
            {
                new AuthorizeDomain("firstLogin", "firstPassword"),
                new AuthorizeDomain("firstLogin", "firstPassword"),
            };
    }
}