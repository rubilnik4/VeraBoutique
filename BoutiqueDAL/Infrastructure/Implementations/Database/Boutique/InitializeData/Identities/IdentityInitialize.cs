using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Identities;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Identities;
using BoutiqueDAL.Models.Implementations.Identities;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.AuthorizeErrors;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData.Identities
{
    /// <summary>
    /// Начальные данные авторизации
    /// </summary>
    public static class IdentityInitialize
    {
        /// <summary>
        /// Добавить роли
        /// </summary>
        public static async Task Initialize(IUserManagerService userManager, IRoleStoreService roleStore,
                                            IEnumerable<BoutiqueRoleUser> defaultUsers,
                                            IEnumerable<IdentityRoleType> defaultRoles) =>
            await CreateRoles(roleStore, defaultRoles).
            ResultErrorBindOkBindAsync(() => CreateUsers(userManager, defaultUsers)).
            ResultErrorVoidBadTaskAsync(_ => throw new ArgumentException("Пользователи по умолчанию не инициализированы"));

        /// <summary>
        /// Добавить роли и обработать ошибки
        /// </summary>
        private static async Task<IResultError> CreateRoles(IRoleStoreService roleStore, IEnumerable<IdentityRoleType> defaultRoles) =>
             await IdentityRolesInitialize.CreateIdentityRoles(roleStore, defaultRoles).
             MapTaskAsync(result => result.Errors.
                                    Where(error => error is not AuthorizeErrorResult { ErrorType: AuthorizeErrorType.Duplicate }).
                                    ToResultError());

        /// <summary>
        /// Добавить пользователей и обработать ошибки
        /// </summary>
        private static async Task<IResultError> CreateUsers(IUserManagerService userManager, IEnumerable<BoutiqueRoleUser> defaultUsers) =>
             await IdentityUsersInitialize.CreateIdentityUsers(userManager, defaultUsers).
             MapTaskAsync(result => result.Errors.
                                    Where(error => error is not AuthorizeErrorResult { ErrorType: AuthorizeErrorType.Duplicate }).
                                    ToResultError());
    }
}