using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueDAL.Models.Implementations.Identity;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Interfaces.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData.Identity
{
    /// <summary>
    /// Инициализация пользователей
    /// </summary>
    public static class IdentityUsersInitialize
    {
        /// <summary>
        /// Проверить и добавить пользователей
        /// </summary>
        public static async Task CreateIdentityUsers(IdentityDbContext<BoutiqueUser> dbContext, IResultCollection<BoutiqueRoleUser> defaultUsers) =>
            await new UserStore<IdentityUser>(dbContext).
            VoidAsync(userStore => GetUsersToCreate(userStore, defaultUsers).
                                   VoidBindAsync(users => CreateUsers(userStore, users)));

        /// <summary>
        /// Получить роли для добавления в базу
        /// </summary>
        private static async Task<IEnumerable<BoutiqueRoleUser>> GetUsersToCreate(UserStore<IdentityUser> userStore,
                                                                              IResultCollection<BoutiqueRoleUser> defaultUsersResult) =>
            await defaultUsersResult.WhereContinue(defaultUsers => defaultUsers.OkStatus,
                okFunc: defaultUsers => userStore.Users.ToListAsync().
                                        MapTaskAsync(users => users.Select(user => user.UserName)).
                                        MapTaskAsync(userNames => GetNewDefaultUsers(defaultUsers.Value, userNames)),
                badFunc: _ => Task.FromResult(Enumerable.Empty<BoutiqueRoleUser>()));

        /// <summary>
        /// Найти новых пользователей
        /// </summary>
        private static IEnumerable<BoutiqueRoleUser> GetNewDefaultUsers(IEnumerable<BoutiqueRoleUser> defaultUsers,
                                                                        IEnumerable<string> userNames) =>
            defaultUsers.Where(defaultUser => !userNames.Contains(defaultUser.BoutiqueUser.UserName, StringComparer.InvariantCultureIgnoreCase));
        
        /// <summary>
        /// Добавить пользователей в базу
        /// </summary>
        private static async Task CreateUsers(UserStore<IdentityUser> userStore, IEnumerable<BoutiqueRoleUser> users)
        {
            foreach (var user in users)
            {
                await userStore.CreateAsync(user.BoutiqueUser);
            }
        }
    }
}