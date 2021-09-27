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
        /// Получить имя пользователя и пароль
        /// </summary>
        public static IReadOnlyCollection<IAuthorizeDomain> AuthorizeDomains =>
            new List<IAuthorizeDomain>
            {
                new AuthorizeDomain("firstLogin@yandex.ru", "firstPassword07"),
                new AuthorizeDomain("firstLogin@yandex.ru", "firstPassword07"),
            };
    }
}