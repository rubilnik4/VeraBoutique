using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Enums.Identity;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueDAL.Models.Implementations.Identity;
using ResultFunctional.FunctionalExtensions.Sync;

namespace BoutiqueMVCXUnit.Data.Identity
{
    /// <summary>
    /// Данные пользователей
    /// </summary>
    public static class IdentityData
    {
        /// <summary>
        /// Пользователи с ролью
        /// </summary>
        public static IReadOnlyCollection<BoutiqueRoleUser> BoutiqueRoleUsers =>
            RegisterData.RegisterDomains.
            Select(BoutiqueIdentityUser.GetBoutiqueUser).
            Select(user => new BoutiqueRoleUser(IdentityRoleType.User, user)).
            ToList();

        /// <summary>
        /// Пользователь с ролью
        /// </summary>
        public static BoutiqueRoleUser BoutiqueRoleUser =>
            BoutiqueRoleUsers.First();
    }
}