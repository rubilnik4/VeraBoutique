using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.TaskExtensions;
using BoutiqueDAL.Models.Implementations.Identity;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.Models.Interfaces.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData.Identity
{
    public static class IdentityAssignRoles
    {
        /// <summary>
        /// Присвоить роли пользователям
        /// </summary>
        public static async Task AssignRoles(UserManager<BoutiqueUser> userManager, IResultCollection<BoutiqueRoleUser> defaultUsersResult) =>
            await defaultUsersResult.
            ResultCollectionOkAsync(defaultUsers => FindUsersInManager(userManager, defaultUsers)).
            ResultCollectionOkBindAsync(usersToAdd => AddUsersToManager(userManager, usersToAdd));

        /// <summary>
        /// Найти существующих пользователей в списке
        /// </summary>
        private static async Task<IEnumerable<BoutiqueRoleUser>> FindUsersInManager(UserManager<BoutiqueUser> userManager,
                                                                                IReadOnlyCollection<BoutiqueRoleUser> defaultUsers) =>
             await userManager.Users.AsNoTracking().ToListAsync().
             MapTaskAsync(users => users.Select(user => user.UserName)).
             MapTaskAsync(userNames => defaultUsers.Where(defaultUser => userNames.Contains(defaultUser.BoutiqueUser.UserName, StringComparer.InvariantCultureIgnoreCase)));

        /// <summary>
        /// Присвоить роли в базе данных
        /// </summary>
        private static async Task<IEnumerable<IdentityResult>> AddUsersToManager(UserManager<BoutiqueUser> userManager,
                                                                                 IEnumerable<BoutiqueRoleUser> users) =>
            await users.
            Select(user => userManager.GetRolesAsync(user.BoutiqueUser).
                           MapBindAsync(userRoles => userManager.RemoveFromRolesAsync(user.BoutiqueUser, userRoles)).
                           MapBindAsync(_ => userManager.AddToRoleAsync(user.BoutiqueUser, user.IdentityRoleType.ToString()))).
            WaitAll().
            MapTaskAsync(identityResults => (IEnumerable<IdentityResult>)identityResults);
    }
}