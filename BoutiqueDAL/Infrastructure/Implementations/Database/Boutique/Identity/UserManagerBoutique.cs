using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Identity;
using BoutiqueDAL.Models.Enums.Identity;
using BoutiqueDAL.Models.Implementations.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResultFunctional.FunctionalExtensions.Async;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Identity
{
    /// <summary>
    /// Менеджер авторизации
    /// </summary>
    public class UserManagerBoutique : UserManager<BoutiqueUser>, IUserManagerBoutique
    {
        public UserManagerBoutique(IUserStore<BoutiqueUser> store, IOptions<IdentityOptions> optionAccessor,
                                   IPasswordHasher<BoutiqueUser> passwordHasher,
                                   IEnumerable<IUserValidator<BoutiqueUser>> userValidators,
                                   IEnumerable<IPasswordValidator<BoutiqueUser>> passwordValidators,
                                   ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
                                   IServiceProvider services, ILogger<UserManager<BoutiqueUser>> logger)
            : base(store, optionAccessor, passwordHasher, userValidators,
                   passwordValidators, keyNormalizer, errors, services, logger)
        { }

        /// <summary>
        /// Зарегистрироваться
        /// </summary>
        public async Task<IdentityResult> Register(IRegisterDomain register, IdentityRoleType roleType) =>
            await BoutiqueUser.GetBoutiqueUser(register).
            MapAsync(CreateAsync);

        /// <summary>
        /// Создать пользователя
        /// </summary>
        public async Task<IdentityResult> Register(BoutiqueUser user, IdentityRoleType roleType) =>
            await CreateAsync(user).
            WhereOkBindAsync(identityResult => identityResult.Succeeded,
                             _ => AddToRoleAsync(user, roleType.ToString()));

        /// <summary>
        /// Найти пользователя по почте
        /// </summary>
        public async Task<BoutiqueUser?> FindByEmail(string email) =>
            await FindByEmailAsync(email);
    }
}