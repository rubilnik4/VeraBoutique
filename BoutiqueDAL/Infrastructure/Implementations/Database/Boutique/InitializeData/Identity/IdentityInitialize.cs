using System.Threading.Tasks;
using BoutiqueDAL.Models.Implementations.Identity;
using Functional.FunctionalExtensions.Async;
using Functional.Models.Interfaces.Result;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData.Identity
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