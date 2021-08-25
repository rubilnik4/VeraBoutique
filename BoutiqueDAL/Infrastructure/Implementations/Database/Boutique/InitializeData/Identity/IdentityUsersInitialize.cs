using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueDAL.Models.Implementations.Identity;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Interfaces.Results;
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
        public static async Task CreateIdentityUsers(IdentityDbContext<IdentityUser> dbContext, IResultCollection<BoutiqueUser> defaultUsers) =>
            await new UserStore<IdentityUser>(dbContext).
            VoidAsync(userStore => GetUsersToCreate(userStore, defaultUsers).
                                   VoidBindAsync(user => CreateUsers(userStore, user)));

        /// <summary>
        /// Получить роли для добавления в базу
        /// </summary>
        private static async Task<IEnumerable<IdentityUser>> GetUsersToCreate(UserStore<IdentityUser> userStore,
                                                                              IResultCollection<BoutiqueUser> defaultUsersResult) =>
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
            defaultUsers.Where(defaultUser => !userNames.Contains(defaultUser.UserName, StringComparer.InvariantCultureIgnoreCase));
        
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
    }
}