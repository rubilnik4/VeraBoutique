using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.TaskExtensions;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueCommon.Models.Enums.Identities;
using BoutiqueDAL.Extensions.Async.Identity;
using BoutiqueDAL.Extensions.Sync.Identity;
using BoutiqueDAL.Infrastructure.Implementations.Identities;
using BoutiqueDAL.Infrastructure.Interfaces.Identities;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Identities;
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
        public UserManagerService(IUserManagerBoutique userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Менеджер авторизации
        /// </summary>
        private readonly IUserManagerBoutique _userManager;

        /// <summary>
        /// Получить пользователей
        /// </summary>
        public async Task<IReadOnlyCollection<BoutiqueIdentityUser>> GetUsers() =>
            await _userManager.Users.ToListAsync();

        /// <summary>
        /// Получить пользователей с ролями
        /// </summary>
        public async Task<IReadOnlyCollection<BoutiqueRoleUser>> GetRoleUsers() =>
            await _userManager.Users.ToListAsync().
            MapBindAsync(users => users.Select(GetRoleUser).WaitAllInLine()).
            MapTaskAsync(users => (IReadOnlyCollection<BoutiqueRoleUser>)users);

        /// <summary>
        /// Получить роли для пользователя
        /// </summary>
        public async Task<BoutiqueRoleUser> GetRoleUser(BoutiqueIdentityUser user) =>
             await _userManager.GetRolesAsync(user).
             MapTaskAsync(roles => roles.Select(Enum.Parse<IdentityRoleType>).FirstOrDefault()).
             MapTaskAsync(roleType => new BoutiqueRoleUser(roleType, user));

        /// <summary>
        /// Найти пользователя с ролью по почте
        /// </summary>
        public async Task<IResultValue<BoutiqueRoleUser>> FindRoleUserByEmail(string email) =>
            await FindUserByEmail(email).
            ResultValueOkBindAsync(GetRoleUser);

        /// <summary>
        /// Найти пользователя по почте
        /// </summary>
        public async Task<IResultValue<BoutiqueIdentityUser>> FindUserByEmail(string email) =>
            await _userManager.FindByEmailAsync(email).
            MapTaskAsync(user => (BoutiqueIdentityUser?)user).
            ToResultValueNullCheckTaskAsync(ErrorResultFactory.ValueNotFoundError(email, GetType()));

        /// <summary>
        /// Зарегистрироваться
        /// </summary>
        public async Task<IResultValue<string>> CreateRoleUser(IRegisterDomain register, IdentityRoleType roleType) =>
            await BoutiqueIdentityUser.GetBoutiqueUser(register).
            MapAsync(user => CreateRoleUser(user, roleType));

        /// <summary>
        /// Создать пользователя
        /// </summary>
        public async Task<IResultValue<string>> CreateRoleUser(BoutiqueIdentityUser user, IdentityRoleType roleType) =>
            await CheckDuplicateUser(user).
            ResultValueOkBindAsync(_userManager.CreateAsync).
            ResultValueBindOkTaskAsync(identityResult => identityResult.ToIdentityResultValue(user.Email)).
            ResultValueBindOkBindAsync(_ => _userManager.AddToRoleAsync(user, roleType.ToString()).
                                       ToIdentityResultValueTaskAsync(user.Email));

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        public async Task<IResultValue<string>> DeleteRoleUser(string email) =>
            await FindRoleUserByEmail(email).
            ResultValueBindOkBindAsync(DeleteRoleUser);

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        public async Task<IResultValue<string>> DeleteRoleUser(BoutiqueIdentityUser user) =>
            await GetRoleUser(user).
            MapBindAsync(DeleteRoleUser);

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        public async Task<IResultValue<string>> DeleteRoleUser(BoutiqueRoleUser roleUser) =>
            await  _userManager.RemoveFromRoleAsync(roleUser.BoutiqueIdentityUser, roleUser.IdentityRoleType.ToString()).
            WhereOkBindAsync(identityResult => identityResult.Succeeded,
                             _ => _userManager.DeleteAsync(roleUser.BoutiqueIdentityUser)).
            ToIdentityResultValueTaskAsync(roleUser.BoutiqueIdentityUser.Email);

        /// <summary>
        /// Проверить пользователя на дублирование
        /// </summary>
        private async Task<IResultValue<BoutiqueIdentityUser>> CheckDuplicateUser(BoutiqueIdentityUser boutiqueUser) =>
            await _userManager.FindByEmailAsync(boutiqueUser.Email).
            WhereContinueTaskAsync(user => user is null!,
                                   _ => boutiqueUser.ToResultValue(),
                                   _ => ErrorResultFactory.AuthorizeError(AuthorizeErrorType.Duplicate, $"Дублирование пользователя {boutiqueUser.Email}").
                                        ToResultValue<BoutiqueIdentityUser>());
    }
}