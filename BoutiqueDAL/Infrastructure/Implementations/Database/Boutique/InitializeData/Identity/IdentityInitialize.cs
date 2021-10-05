using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Identity;
using BoutiqueDAL.Infrastructure.Implementations.Identity;
using BoutiqueDAL.Infrastructure.Interfaces.Identity;
using BoutiqueDAL.Models.Implementations.Identity;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.Models.Interfaces.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.AuthorizeErrors;
using ResultFunctional.Models.Interfaces.Errors.Base;

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
        public static async Task Initialize(IUserManagerBoutique userManager, IRoleStoreBoutique roleStore,
                                            IEnumerable<BoutiqueRoleUser> defaultUsers,
                                            IEnumerable<IdentityRoleType> defaultRoles) =>
            await CreateRoles(roleStore, defaultRoles).
            ResultErrorBindOkBindAsync(() => CreateUsers(userManager, defaultUsers)).
            ResultErrorVoidBadTaskAsync(_ => throw new ArgumentException("Пользователи по умолчанию не инициализированы"));

        /// <summary>
        /// Добавить роли и обработать ошибки
        /// </summary>
        private static async Task<IResultError> CreateRoles(IRoleStoreBoutique roleStore, IEnumerable<IdentityRoleType> defaultRoles) =>
             await IdentityRolesInitialize.CreateIdentityRoles(roleStore, defaultRoles).
             MapTaskAsync(result => result.Errors.
                                    Where(error => error is not AuthorizeErrorResult { ErrorType: AuthorizeErrorType.Duplicate }).
                                    ToResultError());

        /// <summary>
        /// Добавить пользователей и обработать ошибки
        /// </summary>
        private static async Task<IResultError> CreateUsers(IUserManagerBoutique userManager, IEnumerable<BoutiqueRoleUser> defaultUsers) =>
             await IdentityUsersInitialize.CreateIdentityUsers(userManager, defaultUsers).
             MapTaskAsync(result => result.Errors.
                                    Where(error => error is not AuthorizeErrorResult { ErrorType: AuthorizeErrorType.Duplicate }).
                                    ToResultError());
    }
}