using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.TaskExtensions;
using BoutiqueDAL.Models.Enums.Identity;
using BoutiqueDAL.Models.Implementations.Identity;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Interfaces.Result;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
        public static async Task Initialize(IdentityDbContext<IdentityUser> dbContext, UserManager<IdentityUser> userManager,
                                            IResultCollection<BoutiqueUser> defaultUsers) =>
            await dbContext.
            VoidAsync(IdentityRolesInitialize.CreateIdentityRoles).
            VoidBindAsync(_ => IdentityUsersInitialize.CreateIdentityUsers(dbContext, defaultUsers)).
            VoidBindAsync(_ => IdentityAssignRoles.AssignRoles(userManager, defaultUsers));
    }
}