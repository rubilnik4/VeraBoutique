using System;
using System.Collections.Generic;
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
    public class UserManagerBoutique: UserManager<IdentityUser>, IUserManagerBoutique
    {
        public UserManagerBoutique(IUserStore<IdentityUser> store, IOptions<IdentityOptions> optionAccessor, 
                                   IPasswordHasher<IdentityUser> passwordHasher,
                                   IEnumerable<IUserValidator<IdentityUser>> userValidators,
                                   IEnumerable<IPasswordValidator<IdentityUser>> passwordValidators, 
                                   ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, 
                                   IServiceProvider services, ILogger<UserManager<IdentityUser>> logger)
            :base(store, optionAccessor, passwordHasher, userValidators, 
                  passwordValidators, keyNormalizer, errors, services, logger)
        { }
    }
}