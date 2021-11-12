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
    /// Менеджер пользователей
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
            await _userManager.GetUsers().
            MapBindAsync(users => users.SelectAsync(GetRoleUser));

        /// <summary>
        /// Найти пользователя с ролью по почте
        /// </summary>
        public async Task<IResultValue<IBoutiqueUserDomain>> FindRoleUserByEmail(string email) =>
            await FindUserByEmail(email).
            ResultValueOkBindAsync(GetRoleUser);

        /// <summary>
        /// Получить пользователей по роли
        /// </summary>
        public async Task<IReadOnlyCollection<IBoutiqueUserDomain>> GetUsersByRole(IdentityRoleType identityRoleType) =>
            await _userManager.GetUsersInRoleAsync(RoleStoreService.NormalizeRoleName(identityRoleType)).
            MapTaskAsync(users => users.Select(user => user.ToBoutiqueUser(identityRoleType)).ToList());

        /// <summary>
        /// Зарегистрироваться
        /// </summary>
        public async Task<IResultValue<string>> CreateRoleUser(IRegisterRoleDomain registerRole) =>
            await RegisterValidation.RegisterValidate(registerRole, _identitySettings.ToPasswordSettings()).
            ResultValueOk(BoutiqueUserEntity.GetBoutiqueUser).
            ResultValueBindOkAsync(user => CreateRoleUser(user, registerRole.IdentityRoleType));

        /// <summary>
        /// Создать пользователя
        /// </summary>
        public async Task<IResultError> UpdateRoleUser(IBoutiqueUserDomain userRole) =>
            await PersonalValidation.PersonalValidate(userRole).
            ResultValueBindOkAsync(_ => FindRoleUserEntityByEmail(userRole.Email)).
            ResultValueOkTaskAsync(user => user.UpdatePersonal(userRole)).
            ResultValueBindErrorsOkBindAsync(user => UpdateUser(user.BoutiqueUserEntity));

        /// <summary>
        /// Удалить пользователей
        /// </summary>
        public async Task<IResultError> DeleteRoleUsers(IEnumerable<IBoutiqueUserDomain> users) =>
            await users.
            SelectAsync(DeleteRoleUser).
            ToResultCollectionTaskAsync();

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        public async Task<IResultValue<string>> DeleteRoleUser(IBoutiqueUserDomain user) =>
            await DeleteRoleUser(user.Email);

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        public async Task<IResultValue<string>> DeleteRoleUser(string email) =>
            await FindRoleUserEntityByEmail(email).
            ResultValueBindOkBindAsync(DeleteRoleUser);

        /// <summary>
        /// Удалить пользователей
        /// </summary>
        public async Task<IResultError> DeleteRoleUsersByRole(IdentityRoleType identityRoleType) =>
            await _userManager.GetUsersInRoleAsync(RoleStoreService.NormalizeRoleName(identityRoleType)).
            MapBindAsync(users => users.Select(user => new BoutiqueRoleUser(identityRoleType, user)).
                                        SelectAsync(DeleteRoleUser)).
            ToResultCollectionTaskAsync();

        /// <summary>
        /// Получить роли для пользователя
        /// </summary>
        private async Task<IBoutiqueUserDomain> GetRoleUser(BoutiqueUserEntity userEntity) =>
             await GetRoleUserEntity(userEntity).
             MapTaskAsync(roleUser => roleUser.ToBoutiqueUser());

        /// <summary>
        /// Найти пользователя с ролью по почте
        /// </summary>
        public async Task<IResultValue<BoutiqueRoleUser>> FindRoleUserEntityByEmail(string email) =>
            await FindUserByEmail(email).
            ResultValueOkBindAsync(GetRoleUserEntity);

        /// <summary>
        /// Получить роли для пользователя
        /// </summary>
        private async Task<BoutiqueRoleUser> GetRoleUserEntity(BoutiqueUserEntity userEntity) =>
             await _userManager.GetRolesAsync(userEntity).
             MapTaskAsync(roles => roles.Select(Enum.Parse<IdentityRoleType>).FirstOrDefault()).
             MapTaskAsync(identityRoleType => new BoutiqueRoleUser(identityRoleType, userEntity));

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
        /// Обновить пользователя
        /// </summary>
        private async Task<IResultError> UpdateUser(BoutiqueUserEntity userEntity) =>
            await _userManager.UpdateAsync(userEntity).
            MapTaskAsync(identityResult => identityResult.ToIdentityResultValue(userEntity.Email));

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        private async Task<IResultValue<string>> DeleteRoleUser(BoutiqueRoleUser roleUser) =>            
             await _userManager.RemoveFromRoleAsync(roleUser.BoutiqueUserEntity, RoleStoreService.NormalizeRoleName(roleUser.IdentityRoleType)).
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
                                   _ => ErrorResultFactory.AuthorizeError(AuthorizeErrorType.Duplicate, "Пользователь уже присутствует в системе").
                                        ToResultValue<BoutiqueUserEntity>());
    }
}