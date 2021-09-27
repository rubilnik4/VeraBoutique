using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueDAL.Models.Implementations.Identity;
using BoutiqueMVC.Models.Interfaces.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResultFunctional.FunctionalExtensions.Async;

namespace BoutiqueMVC.Models.Implementations.Identity
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
        public async Task<IdentityResult> Register(IRegisterDomain register) =>
            await BoutiqueUser.GetBoutiqueUser(register).
            MapAsync(CreateAsync);

        /// <summary>
        /// Найти пользователя по почте
        /// </summary>
        public async Task<BoutiqueUser?> FindByEmail(string email) =>
            await FindByEmailAsync(email);
    }
}