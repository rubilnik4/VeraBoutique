using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Identities;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueCommon.Models.Enums.Identities;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueDAL.Models.Implementations.Entities.Identities;
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
            Select(BoutiqueUserEntity.GetBoutiqueUser).
            Select(user => new BoutiqueRoleUser(IdentityRoleType.User, user)).
            ToList();

        /// <summary>
        /// Пользователи с ролью
        /// </summary>
        public static IReadOnlyCollection<IBoutiqueUserDomain> BoutiqueUsers =>
            BoutiqueRoleUsers.
            Select(roleUser => roleUser.ToBoutiqueUser()).
            ToList();

        /// <summary>
        /// Пользователи
        /// </summary>
        public static IReadOnlyCollection<BoutiqueUserEntity> BoutiqueIdentityUsers =>
            BoutiqueRoleUsers.
            Select(roleUser => roleUser.BoutiqueUserEntity).
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

        /// <summary>
        /// Дублирование роли
        /// </summary>
        public static string DuplicateRoleName =>
             "DuplicateRoleName";

        /// <summary>
        /// Дублирование пользователя
        /// </summary>
        public static string DuplicateUserName =>
             "DuplicateUserName";

        /// <summary>
        /// Параметры авторизации
        /// </summary>
        public static IdentitySettings IdentitySettings =>
            new(8, true, false, true);
    }
}