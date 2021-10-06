using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Enums.Identities;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueDAL.Models.Implementations.Identities;

namespace BoutiqueDALXUnit.Data.Identity
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
        /// Пользователи
        /// </summary>
        public static IReadOnlyCollection<BoutiqueIdentityUser> BoutiqueIdentityUsers =>
            BoutiqueRoleUsers.
            Select(roleUser => roleUser.BoutiqueIdentityUser).
            ToList();

        /// <summary>
        /// Роли
        /// </summary>
        public static IReadOnlyCollection<IdentityRoleType> Roles =>
            Enum.GetValues<IdentityRoleType>().
            Where(role => role != IdentityRoleType.Unknown).
            ToList();

        /// <summary>
        /// Роли
        /// </summary>
        public static IReadOnlyCollection<string> RoleNames =>
            Roles.
            Select(role => role.ToString()).
            ToList();
    }
}