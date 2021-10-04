using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.TaskExtensions;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueCommon.Models.Enums.Identity;
using BoutiqueDAL.Extensions.Async.Identity;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Identity;
using BoutiqueDAL.Models.Enums.Identity;
using BoutiqueDAL.Models.Implementations.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Identity
{
    /// <summary>
    /// Менеджер авторизации
    /// </summary>
    public class UserManagerBoutique : UserManager<BoutiqueIdentityUser>, IUserManagerBoutique
    {
        public UserManagerBoutique(IUserStore<BoutiqueIdentityUser> store, IOptions<IdentityOptions> optionAccessor,
                                   IPasswordHasher<BoutiqueIdentityUser> passwordHasher,
                                   IEnumerable<IUserValidator<BoutiqueIdentityUser>> userValidators,
                                   IEnumerable<IPasswordValidator<BoutiqueIdentityUser>> passwordValidators,
                                   ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
                                   IServiceProvider services, ILogger<UserManager<BoutiqueIdentityUser>> logger)
            : base(store, optionAccessor, passwordHasher, userValidators,
                   passwordValidators, keyNormalizer, errors, services, logger)
        { }

        /// <summary>
        /// Получить пользователей
        /// </summary>
        public async Task<IReadOnlyCollection<BoutiqueIdentityUser>> GetUsers() =>
            await Users.ToListAsync();

        /// <summary>
        /// Получить пользователей с ролями
        /// </summary>
        public async Task<IReadOnlyCollection<BoutiqueRoleUser>> GetRoleUsers() =>
            await Users.ToListAsync().
            MapBindAsync(users => users.Select(GetRoleUser).WaitAllInLine()).
            MapTaskAsync(users => (IReadOnlyCollection<BoutiqueRoleUser>)users);

        /// <summary>
        /// Получить роли для пользователя
        /// </summary>
        public async Task<BoutiqueRoleUser> GetRoleUser(BoutiqueIdentityUser user) =>
             await GetRolesAsync(user).
             MapTaskAsync(roles => roles.Select(Enum.Parse<IdentityRoleType>).FirstOrDefault()).
             MapTaskAsync(roleType => new BoutiqueRoleUser(roleType, user));

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        public async Task<IResultValue<string>> DeleteRoleUser(BoutiqueIdentityUser user) =>
            await GetRoleUser(user).
            MapBindAsync(roleUser => RemoveFromRoleAsync(roleUser.BoutiqueIdentityUser, roleUser.IdentityRoleType.ToString())).
            WhereOkBindAsync(identityResult => identityResult.Succeeded,
                             _ => DeleteAsync(user)).
            ToIdentityResultValueTaskAsync(user.Email);

        /// <summary>
        /// Получить роли пользователей
        /// </summary>
        public async Task<IList<string>> GetRoles(BoutiqueIdentityUser user) =>
            await GetRolesAsync(user);

        /// <summary>
        /// Зарегистрироваться
        /// </summary>
        public async Task<IResultValue<string>> Register(IRegisterDomain register, IdentityRoleType roleType) =>
            await BoutiqueIdentityUser.GetBoutiqueUser(register).
            MapAsync(user => Register(user, roleType));

        /// <summary>
        /// Создать пользователя
        /// </summary>
        public async Task<IResultValue<string>> Register(BoutiqueIdentityUser user, IdentityRoleType roleType) =>
            await CreateAsync(user).
            ToIdentityResultValueTaskAsync(user.Email).
            ResultValueBindOkBindAsync(_ => AddToRoleAsync(user, roleType.ToString()).
                                       ToIdentityResultValueTaskAsync(user.Email));

        /// <summary>
        /// Найти пользователя по почте
        /// </summary>
        public async Task<IResultValue<BoutiqueIdentityUser>> FindByEmail(string email) =>
            await FindByEmailAsync(email).
            MapTaskAsync(user => (BoutiqueIdentityUser?)user).
            ToResultValueNullCheckTaskAsync(ErrorResultFactory.ValueNotFoundError(email, GetType()));
    }
}