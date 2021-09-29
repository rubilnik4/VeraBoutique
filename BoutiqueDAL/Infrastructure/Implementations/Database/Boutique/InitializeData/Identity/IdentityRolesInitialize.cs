using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.TaskExtensions;
using BoutiqueDAL.Models.Enums.Identity;
using BoutiqueDAL.Models.Implementations.Identity;
using ResultFunctional.FunctionalExtensions.Async;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData.Identity
{
    /// <summary>
    /// Инициализация ролей
    /// </summary>
    public static class IdentityRolesInitialize
    {
        /// <summary>
        /// Проверить и добавить роли
        /// </summary>
        public static async Task<IEnumerable<IdentityResult>> CreateIdentityRoles(IRoleStore<IdentityRole> roleStore,
                                                                                          IReadOnlyCollection<string> defaultRoles) =>
            await GetRoles(roleStore, defaultRoles).
            MapBindAsync(roles => CreateRoles(roleStore, roles));

        /// <summary>
        /// Получить роли для добавления в базу
        /// </summary>
        private static async Task<IEnumerable<string>> GetRoles(IRoleStore<IdentityRole> roleStore,
                                                                IReadOnlyCollection<string> defaultRoles) =>
            await defaultRoles.
            Select(role => roleStore.FindByNameAsync(NormalizeRoleName(role), default)).
            WaitAllInLine().
            MapTaskAsync(findUsers => GetRoles(findUsers, defaultRoles));

        /// <summary>
        /// Выбрать ненайденные роли
        /// </summary>
        private static IEnumerable<string> GetRoles(IEnumerable<IdentityRole?> findRoles,
                                                              IEnumerable<string> defaultRoles) =>
             findRoles.
             Zip(defaultRoles, (findRole, defaultRole) => (findRole, defaultRole)).
             Where(x => x.findRole is null).
             Select(x => x.defaultRole);

        /// <summary>
        /// Добавить роли в базу
        /// </summary>
        private static async Task<IEnumerable<IdentityResult>> CreateRoles(IRoleStore<IdentityRole> roleStore, 
                                                                           IEnumerable<string> roles) =>
            await roles.
            Select(role => new IdentityRole(role) { NormalizedName = NormalizeRoleName(role) }).
            Select(identityRole => roleStore.CreateAsync(identityRole, default)).
            WaitAllInLine();

        /// <summary>
        /// Нормализовать имя роли
        /// </summary>
        private static string NormalizeRoleName(string roleName) =>
            roleName.ToUpperInvariant();
    }
}