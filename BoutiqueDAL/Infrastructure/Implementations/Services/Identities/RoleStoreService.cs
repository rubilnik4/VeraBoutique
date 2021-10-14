using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Identities;
using BoutiqueDAL.Extensions.Sync.Identity;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Database;
using BoutiqueDAL.Infrastructure.Interfaces.Identities;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.Identities
{
    /// <summary>
    /// Управление ролями
    /// </summary>
    public class RoleStoreService : IRoleStoreService
    {
        public RoleStoreService(IRoleStoreBoutique roleStore)
        {
            _roleStore = roleStore;
        }

        /// <summary>
        /// Хранилище ролей
        /// </summary>
        private readonly IRoleStoreBoutique _roleStore;

        /// <summary>
        /// Получить роли
        /// </summary>
        public async Task<IReadOnlyCollection<string>> GetRoles() =>
            await _roleStore.GetRoles().
            MapTaskAsync(roles => roles.Select(role => role.Name).ToList());

        /// <summary>
        /// Создать роль
        /// </summary>
        public async Task<IResultValue<IdentityRoleType>> CreateRole(IdentityRoleType identityRoleType) =>
            await identityRoleType.
            Map(GetIdentityRole).
            MapAsync(CheckDuplicateRole).
            ResultValueOkBindAsync(identityRole => _roleStore.CreateAsync(identityRole)).
            ResultValueBindOkTaskAsync(identityResult => identityResult.ToIdentityResultValue(identityRoleType));

        /// <summary>
        /// Преобразовать в роль
        /// </summary>
        public static IdentityRole GetIdentityRole(IdentityRoleType identityRoleType) =>
            new (identityRoleType.ToString())
            {
                NormalizedName = NormalizeRoleName(identityRoleType.ToString())
            };

        /// <summary>
        /// Проверить роль на дублирование
        /// </summary>
        private async Task<IResultValue<IdentityRole>> CheckDuplicateRole(IdentityRole identityRole) =>
            await _roleStore.FindByNameAsync(identityRole.NormalizedName).
            WhereContinueTaskAsync(roleId => roleId is null,
                                   _ => identityRole.ToResultValue(),
                                   _ => ErrorResultFactory.AuthorizeError(AuthorizeErrorType.Duplicate, $"Дублирование роли {identityRole.Name}").
                                        ToResultValue<IdentityRole>());

        /// <summary>
        /// Нормализовать имя роли
        /// </summary>
        public static string NormalizeRoleName(IdentityRoleType identityRoleType) =>
            NormalizeRoleName(identityRoleType.ToString());

        /// <summary>
        /// Нормализовать имя роли
        /// </summary>
        public static string NormalizeRoleName(string roleName) =>
            roleName.ToUpperInvariant();
    }
}