using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.TaskExtensions;
using BoutiqueDAL.Models.Implementations.Identity;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using Functional.Models.Interfaces.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData.Identity
{
    public static class IdentityAssignRoles
    {
        /// <summary>
        /// Присвоить роли пользователям
        /// </summary>
        public static async Task AssignRoles(UserManager<IdentityUser> userManager, IResultCollection<BoutiqueUser> defaultUsersResult) =>
            await defaultUsersResult.
            ResultCollectionOkAsync(defaultUsers => FindUsersInManager(userManager, defaultUsers)).
            ResultCollectionOkBindAsync(usersToAdd => AddUsersToManager(userManager, usersToAdd));

        /// <summary>
        /// Найти существующих пользователей в списке
        /// </summary>
        private static async Task<IEnumerable<BoutiqueUser>> FindUsersInManager(UserManager<IdentityUser> userManager,
                                                                                IReadOnlyCollection<BoutiqueUser> defaultUsers) =>
             await userManager.Users.AsNoTracking().ToListAsync().
             MapTaskAsync(users => users.Select(user => user.UserName)).
             MapTaskAsync(userNames => defaultUsers.Where(defaultUser => userNames.Contains(defaultUser.UserName,
                                                                                            StringComparer.InvariantCultureIgnoreCase)));

        /// <summary>
        /// Присвоить роли в базе данных
        /// </summary>
        private static async Task<IEnumerable<IdentityResult>> AddUsersToManager(UserManager<IdentityUser> userManager,
                                                                                 IEnumerable<BoutiqueUser> users) =>
            await users.
            Select(user => userManager.GetRolesAsync(user).
                           MapBindAsync(userRoles => userManager.RemoveFromRolesAsync(user, userRoles)).
                           MapBindAsync(_ => userManager.AddToRoleAsync(user, user.IdentityRoleType))).
            WaitAll().
            MapTaskAsync(identityResults => (IEnumerable<IdentityResult>)identityResults);
    }
}