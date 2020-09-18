using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using BoutiqueDAL.Models.Enums.Identity;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultError;
using Functional.Models.Interfaces.Result;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData
{
    /// <summary>
    /// Начальные данные авторизации
    /// </summary>
    public static class IdentityInitialize
    {
        /// <summary>
        /// Добавить роли
        /// </summary>
        public static async Task Initialize(IdentityDbContext<IdentityUser> dbContext, IResultCollection<IdentityUser> defaultUser) =>
            await dbContext.
            VoidAsync(CreateIdentityRoles).
            VoidBindAsync(_ => CreateIdentityUsers(dbContext, defaultUser));

        /// <summary>
        /// Проверить и добавить роли
        /// </summary>
        private static async Task CreateIdentityRoles(IdentityDbContext<IdentityUser> dbContext) =>
            await new RoleStore<IdentityRole>(dbContext).
            VoidAsync(roleStore => GetRolesToCreate(roleStore).
                                   VoidBindAsync(roles => CreateRoles(roleStore, roles)));

        /// <summary>
        /// Проверить и добавить пользователей
        /// </summary>
        private static async Task CreateIdentityUsers(IdentityDbContext<IdentityUser> dbContext, IResultCollection<IdentityUser> defaultUsers) =>
            await new UserStore<IdentityUser>(dbContext).
            VoidAsync(userStore => GetUsersToCreate(userStore, defaultUsers).
                                   VoidBindAsync(user => CreateUsers(userStore, user)));

        /// <summary>
        /// Получить роли для добавления в базу
        /// </summary>
        private static async Task<IEnumerable<IdentityRole>> GetRolesToCreate(RoleStore<IdentityRole> roleStore) =>
            await roleStore.Roles.ToListAsync().
            MapTaskAsync(roles => roles.Select(role => role.Name)).
            MapTaskAsync(rolesNames => Enum.GetNames(typeof(IdentityRoleType)).
                                       Where(roleType => !rolesNames.Contains(roleType, StringComparer.OrdinalIgnoreCase)).
                                       Select(roleType => new IdentityRole(roleType)));

        /// <summary>
        /// Получить роли для добавления в базу
        /// </summary>
        private static async Task<IEnumerable<IdentityUser>> GetUsersToCreate(UserStore<IdentityUser> userStore, IResultCollection<IdentityUser> defaultUsersResult) =>
            await defaultUsersResult.WhereContinue(defaultUsers => defaultUsers.OkStatus,
                okFunc: defaultUsers => userStore.Users.ToListAsync().
                                        MapTaskAsync(users => users.Select(user => user.UserName)).
                                        MapTaskAsync(userNames => GetNewDefaultUsers(defaultUsers.Value, userNames)),
                badFunc: _ => Task.FromResult(Enumerable.Empty<IdentityUser>()));

        /// <summary>
        /// Найти новых пользователей
        /// </summary>
        private static IEnumerable<IdentityUser> GetNewDefaultUsers(IEnumerable<IdentityUser> defaultUsers,
                                                                    IEnumerable<string> userNames) =>
            defaultUsers.Where(defaultUser => !userNames.Contains(defaultUser.UserName, StringComparer.OrdinalIgnoreCase));

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

        /// <summary>
        /// Добавить пользователей в базу
        /// </summary>
        private static async Task CreateUsers(UserStore<IdentityUser> userStore, IEnumerable<IdentityUser> users)
        {
            foreach (var user in users)
            {
                await userStore.CreateAsync(user);
            }
        }




        //var user = 


        //if (!context.Users.Any(u => u.UserName == user.UserName))
        //{


        //    var userStore = new UserStore<ApplicationUser>(context);
        //    var result = userStore.CreateAsync(user);

        //}

        //AssignRoles(serviceProvider, user.Email, roles);

        //context.SaveChangesAsync();
        //  }

        //public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string[] roles)
        //{
        //    UserManager<ApplicationUser> _userManager = services.GetService<UserManager<ApplicationUser>>();
        //    ApplicationUser user = await _userManager.FindByEmailAsync(email);
        //    var result = await _userManager.AddToRolesAsync(user, roles);

        //    return result;
        //}
    }
}