using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Identity;
using BoutiqueDAL.Extensions.Async.Identity;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Database;
using BoutiqueDAL.Infrastructure.Interfaces.Identity;
using BoutiqueDAL.Models.Implementations.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Implementations.Identity
{
    public class RoleStoreBoutique : RoleStore<IdentityRole>, IRoleStoreBoutique
    {
        public RoleStoreBoutique(BoutiqueDatabase dbContext)
            : base(dbContext)
        { }

        /// <summary>
        /// Создать роль
        /// </summary>
        public async Task<IResultValue<IdentityRoleType>> CreateRole(IdentityRoleType identityRoleType) =>
            await identityRoleType.ToString().
            Map(role => new IdentityRole(role) { NormalizedName = NormalizeRoleName(role) }).
            MapAsync(identityRole => CreateAsync(identityRole)).
            ToIdentityResultValueTaskAsync(identityRoleType);

        /// <summary>
        /// Нормализовать имя роли
        /// </summary>
        private static string NormalizeRoleName(string roleName) =>
            roleName.ToUpperInvariant();
    }
}