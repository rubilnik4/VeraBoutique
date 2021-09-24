using System;
using System.Collections.Generic;
using BoutiqueDAL.Models.Implementations.Identity;
using BoutiqueMVC.Models.Interfaces.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
    }
}