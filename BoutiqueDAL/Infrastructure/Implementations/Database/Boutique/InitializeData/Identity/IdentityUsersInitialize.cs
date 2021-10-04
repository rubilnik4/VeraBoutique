using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.TaskExtensions;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Identity;
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
        public static async Task<IEnumerable<IdentityResult>> CreateIdentityUsers(IUserManagerBoutique userManager, 
                                                                                  IReadOnlyCollection<BoutiqueRoleUser> defaultUsers) =>
            await GetUsers(userManager, defaultUsers).
            MapBindAsync(users => CreateUsers(userManager, users));

        /// <summary>
        /// Получить роли для добавления в базу
        /// </summary>
        public static async Task<IEnumerable<BoutiqueRoleUser>> GetUsers(IUserManagerBoutique userManager,
                                                                         IReadOnlyCollection<BoutiqueRoleUser> defaultUsers) =>
            await defaultUsers.
            Select(user => userManager.FindByEmail(user.BoutiqueIdentityUser.Email)).
            WaitAllInLine().
            MapTaskAsync(findUsers => GetUsers(findUsers, defaultUsers));

        /// <summary>
        /// Выбрать ненайденные роли
        /// </summary>
        private static IEnumerable<BoutiqueRoleUser> GetUsers(IEnumerable<BoutiqueIdentityUser?> findUsers,
                                                              IEnumerable<BoutiqueRoleUser> defaultUsers) =>
             findUsers.
             Zip(defaultUsers, (findUser, defaultUser) => (findUser, defaultUser)).
             Where(x => x.findUser is null).
             Select(x => x.defaultUser);

        /// <summary>
        /// Добавить пользователей в базу
        /// </summary>
        private static async Task<IEnumerable<IdentityResult>> CreateUsers(IUserManagerBoutique userManager,
                                                                           IEnumerable<BoutiqueRoleUser> users) =>
            await users.
            Select(user => userManager.Register(user.BoutiqueIdentityUser, user.IdentityRoleType)).
            WaitAllInLine();
    }
}