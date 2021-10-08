using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Identities;
using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueCommon.Models.Enums.Identities;

namespace BoutiqueCommonXUnit.Data.Authorize
{
    /// <summary>
    /// Регистрация
    /// </summary>
    public static class RegisterData
    {
        /// <summary>
        /// Получить регистрацию
        /// </summary>
        public static IReadOnlyCollection<IRegisterRoleDomain> RegisterRoleDomains =>
            new List<IRegisterRoleDomain>
            {
                new RegisterRoleDomain(AuthorizeData.AuthorizeDomains.First(), PersonalData.PersonalDomains.First(), IdentityRoleType.User),
                new RegisterRoleDomain(AuthorizeData.AuthorizeDomains.Last(), PersonalData.PersonalDomains.Last(), IdentityRoleType.Admin),
            };

        /// <summary>
        /// Получить регистрацию
        /// </summary>
        public static IReadOnlyCollection<IRegisterDomain> RegisterDomains =>
            RegisterRoleDomains;

    }
}