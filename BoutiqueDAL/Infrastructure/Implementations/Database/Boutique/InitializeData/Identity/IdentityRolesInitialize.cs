using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueDAL.Models.Enums.Identity;
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
        public static async Task CreateIdentityRoles(IdentityDbContext<IdentityUser> dbContext) =>
            await new RoleStore<IdentityRole>(dbContext).
            VoidAsync(roleStore => GetRolesToCreate(roleStore).
                                   VoidBindAsync(roles => CreateRoles(roleStore, roles)));

        /// <summary>
        /// Получить роли для добавления в базу
        /// </summary>
        private static async Task<IEnumerable<IdentityRole>> GetRolesToCreate(RoleStore<IdentityRole> roleStore) =>
            await roleStore.Roles.ToListAsync().
            MapTaskAsync(roles => roles.Select(role => role.Name)).
            MapTaskAsync(rolesNames => Enum.GetValues<IdentityRoleType>().
                                       Select(roleType => roleType.ToString()).
                                       Where(roleType => !rolesNames.Contains(roleType, StringComparer.OrdinalIgnoreCase)).
                                       Select(roleType => new IdentityRole(roleType) { NormalizedName = roleType.ToUpperInvariant() }));

        /// <summary>
        /// Добавить роли в базу
        /// </summary>
        private static async Task CreateRoles(RoleStore<IdentityRole> roleStore, IEnumerable<IdentityRole> roles)
        {
            foreach (var role in roles)
            {
                await roleStore.CreateAsync(role);
            }
        }
    }
}