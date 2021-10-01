using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Identity;
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
        public static async Task Initialize(UserManagerBoutique userManager, IRoleStore<IdentityRole> roleStore,
                                            IReadOnlyCollection<BoutiqueRoleUser> defaultUsers, 
                                            IReadOnlyCollection<string> defaultRoles) =>
            await defaultRoles.
            MapAsync(_ => IdentityRolesInitialize.CreateIdentityRoles(roleStore, defaultRoles)).
            MapBindAsync(identitiesResults => IdentityUsersInitialize.CreateIdentityUsers(userManager, defaultUsers).
                                              MapTaskAsync(identitiesResults.Concat)).
            WhereOkTaskAsync(identitiesResults => identitiesResults.Any(identityResult => !identityResult.Succeeded),
                              _ => throw new ArgumentException("Пользователи по умолчанию не инициализированы"));
    }
}