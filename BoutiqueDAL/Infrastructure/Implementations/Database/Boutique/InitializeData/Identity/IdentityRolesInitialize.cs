using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.TaskExtensions;
using BoutiqueCommon.Models.Enums.Identity;
using BoutiqueDAL.Infrastructure.Interfaces.Identity;
using BoutiqueDAL.Models.Enums.Identity;
using BoutiqueDAL.Models.Implementations.Identity;
using ResultFunctional.FunctionalExtensions.Async;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData.Identity
{
    /// <summary>
    /// Инициализация ролей
    /// </summary>
    public static class IdentityRolesInitialize
    {
        /// <summary>
        /// Проверить и добавить роли
        /// </summary>
        public static async Task<IResultCollection<IdentityRoleType>> CreateIdentityRoles(IRoleStoreBoutique roleStore,
                                                                                          IEnumerable<IdentityRoleType> defaultRoles) =>
            await defaultRoles.
            Select(roleStore.CreateRole).
            WaitAllInLine().
            ToResultCollectionTaskAsync();
    }
}