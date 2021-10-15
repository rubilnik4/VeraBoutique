using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Identities;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueCommon.Models.Enums.Identities;

namespace BoutiqueCommonXUnit.Data.Authorize
{
    /// <summary>
    /// Данные пользователей
    /// </summary>
    public static class IdentityData
    {
        /// <summary>
        /// Пользователи с ролью
        /// </summary>
        public static IReadOnlyCollection<IBoutiqueUserDomain> BoutiqueUsers =>
            RegisterData.RegisterRoleDomains.
            Select(register => register.ToBoutiqueUser()).
            ToList();
    }
}