using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.TaskExtensions;
using BoutiqueCommon.Models.Enums.Identities;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Identities;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData.Identities
{
    /// <summary>
    /// Инициализация ролей
    /// </summary>
    public static class IdentityRolesInitialize
    {
        /// <summary>
        /// Проверить и добавить роли
        /// </summary>
        public static async Task<IResultCollection<IdentityRoleType>> CreateIdentityRoles(IRoleStoreService roleStore,
                                                                                          IEnumerable<IdentityRoleType> defaultRoles) =>
            await defaultRoles.
            Select(roleStore.CreateRole).
            WaitAllInLine().
            ToResultCollectionTaskAsync();
    }
}