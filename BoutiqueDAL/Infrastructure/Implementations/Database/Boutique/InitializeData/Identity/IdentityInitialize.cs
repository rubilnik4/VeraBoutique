using System.Threading.Tasks;
using BoutiqueDAL.Models.Implementations.Identity;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.Models.Interfaces.Results;
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
        public static async Task Initialize(IdentityDbContext<BoutiqueUser> dbContext, UserManager<BoutiqueUser> userManager,
                                            IResultCollection<BoutiqueRoleUser> defaultUsers) =>
            await dbContext.
            VoidAsync(IdentityRolesInitialize.CreateIdentityRoles).
            VoidBindAsync(_ => IdentityUsersInitialize.CreateIdentityUsers(dbContext, defaultUsers)).
            VoidBindAsync(_ => IdentityAssignRoles.AssignRoles(userManager, defaultUsers));
    }
}