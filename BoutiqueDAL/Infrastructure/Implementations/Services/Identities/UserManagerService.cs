using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.TaskExtensions;
using BoutiqueCommon.Infrastructure.Implementation.Validation.Identities;
using BoutiqueCommon.Models.Domain.Implementations.Identities;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueCommon.Models.Enums.Identities;
using BoutiqueDAL.Extensions.Async.Identity;
using BoutiqueDAL.Extensions.Sync.Identity;
using BoutiqueDAL.Infrastructure.Implementations.Identities;
using BoutiqueDAL.Infrastructure.Interfaces.Identities;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Identities;
using BoutiqueDAL.Models.Implementations.Entities.Identities;
using BoutiqueDAL.Models.Implementations.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.Identities
{
    /// <summary>
    /// Менеджер авторизации
    /// </summary>
    public class UserManagerService : IUserManagerService
    {
        public UserManagerService(IUserManagerBoutique userManager, IdentitySettings identitySettings)
        {
            _userManager = userManager;
            _identitySettings = identitySettings;
        }

        /// <summary>
        /// Менеджер авторизации
        /// </summary>
        private readonly IUserManagerBoutique _userManager;

        /// <summary>
        /// Параметры авторизации
        /// </summary>
        private readonly IdentitySettings _identitySettings;

        /// <summary>
        /// Получить пользователей с ролями
        /// </summary>
        public async Task<IReadOnlyCollection<IBoutiqueUserDomain>> GetRoleUsers() =>
            await _userManager.Users.ToListAsync().
            MapBindAsync(users => users.Select(GetRoleUser).WaitAllInLine()).
            MapTaskAsync(users => (IReadOnlyCollection<IBoutiqueUserDomain>)users);

        /// <summary>
        /// Найти пользователя с ролью по почте
        /// </summary>
        public async Task<IResultValue<IBoutiqueUserDomain>> FindRoleUserByEmail(string email) =>
            await FindUserByEmail(email).
            ResultValueOkBindAsync(GetRoleUser);

        /// <summary>
        /// Зарегистрироваться
        /// </summary>
        public async Task<IResultValue<string>> CreateRoleUser(IRegisterRoleDomain registerRole) =>
            await RegisterValidation.RegisterValidate(registerRole, _identitySettings.ToPasswordSettings()).
            ResultValueOk(BoutiqueUserEntity.GetBoutiqueUser).
            ResultValueBindOkAsync(user => CreateRoleUser(user, registerRole.IdentityRoleType));

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        public async Task<IResultValue<string>> DeleteRoleUser(string email) =>
            await FindRoleUserByEmail(email).
            ResultValueBindOkBindAsync(DeleteRoleUser);

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        public async Task<IResultValue<string>> DeleteRoleUser(IBoutiqueUserDomain user) =>
            await BoutiqueRoleUser.GetBoutiqueUser(user).
            MapAsync(DeleteRoleUser);

        /// <summary>
        /// Получить роли для пользователя
        /// </summary>
        private async Task<IBoutiqueUserDomain> GetRoleUser(BoutiqueUserEntity userEntity) =>
             await _userManager.GetRolesAsync(userEntity).
             MapTaskAsync(roles => roles.Select(Enum.Parse<IdentityRoleType>).FirstOrDefault()).
             MapTaskAsync(userEntity.ToBoutiqueUser);

        /// <summary>
        /// Найти пользователя по почте
        /// </summary>
        private async Task<IResultValue<BoutiqueUserEntity>> FindUserByEmail(string email) =>
            await _userManager.FindByEmailAsync(email).
            MapTaskAsync(user => (BoutiqueUserEntity?)user).
            ToResultValueNullCheckTaskAsync(ErrorResultFactory.ValueNotFoundError(email, GetType()));

        /// <summary>
        /// Создать пользователя
        /// </summary>
        private async Task<IResultValue<string>> CreateRoleUser(BoutiqueUserEntity userEntity, IdentityRoleType roleType) =>
            await CheckDuplicateUser(userEntity).
            ResultValueOkBindAsync(_userManager.CreateAsync).
            ResultValueBindOkTaskAsync(identityResult => identityResult.ToIdentityResultValue(userEntity.Email)).
            ResultValueBindOkBindAsync(_ => _userManager.AddToRoleAsync(userEntity, roleType.ToString()).
                                       ToIdentityResultValueTaskAsync(userEntity.Email));

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        private async Task<IResultValue<string>> DeleteRoleUser(BoutiqueRoleUser roleUser) =>
            await _userManager.RemoveFromRoleAsync(roleUser.BoutiqueUserEntity, roleUser.IdentityRoleType.ToString()).
            WhereOkBindAsync(identityResult => identityResult.Succeeded,
                             _ => _userManager.DeleteAsync(roleUser.BoutiqueUserEntity)).
            ToIdentityResultValueTaskAsync(roleUser.BoutiqueUserEntity.Email);

        /// <summary>
        /// Проверить пользователя на дублирование
        /// </summary>
        private async Task<IResultValue<BoutiqueUserEntity>> CheckDuplicateUser(BoutiqueUserEntity boutiqueUserEntity) =>
            await _userManager.FindByEmailAsync(boutiqueUserEntity.Email).
            WhereContinueTaskAsync(user => user is null!,
                                   _ => boutiqueUserEntity.ToResultValue(),
                                   _ => ErrorResultFactory.AuthorizeError(AuthorizeErrorType.Duplicate, $"Дублирование пользователя {boutiqueUserEntity.Email}").
                                        ToResultValue<BoutiqueUserEntity>());
    }
}