using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.TaskExtensions;
using BoutiqueDAL.Infrastructure.Interfaces.Identity;
using BoutiqueDAL.Models.Implementations.Identity;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Interfaces.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;

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
        public static async Task<IResultCollection<string>> CreateIdentityUsers(IUserManagerBoutique userManager, 
                                                                                IEnumerable<BoutiqueRoleUser> defaultUsers) =>
            await defaultUsers.
            Select(user => userManager.CreateRoleUser(user.BoutiqueIdentityUser, user.IdentityRoleType)).
            WaitAllInLine().
            ToResultCollectionTaskAsync();
    }
}