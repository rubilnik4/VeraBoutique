using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;

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
        public static IReadOnlyCollection<IRegisterDomain> RegisterDomains =>
            new List<IRegisterDomain>
            {
                new RegisterDomain(AuthorizeData.AuthorizeDomains.First(), PersonalData.PersonalDomains.First()),
                new RegisterDomain(AuthorizeData.AuthorizeDomains.Last(), PersonalData.PersonalDomains.Last()),
            };
    }
}