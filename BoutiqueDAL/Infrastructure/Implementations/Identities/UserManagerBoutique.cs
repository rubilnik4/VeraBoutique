using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.TaskExtensions;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueCommon.Models.Enums.Identities;
using BoutiqueDAL.Extensions.Async.Identity;
using BoutiqueDAL.Extensions.Sync.Identity;
using BoutiqueDAL.Infrastructure.Interfaces.Identities;
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

namespace BoutiqueDAL.Infrastructure.Implementations.Identities
{
    /// <summary>
    /// Менеджер авторизации
    /// </summary>
    public class UserManagerBoutique : UserManager<BoutiqueUserEntity>, IUserManagerBoutique
    {
        public UserManagerBoutique(IUserStore<BoutiqueUserEntity> store, IOptions<IdentityOptions> optionAccessor,
                                   IPasswordHasher<BoutiqueUserEntity> passwordHasher,
                                   IEnumerable<IUserValidator<BoutiqueUserEntity>> userValidators,
                                   IEnumerable<IPasswordValidator<BoutiqueUserEntity>> passwordValidators,
                                   ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
                                   IServiceProvider services, ILogger<UserManager<BoutiqueUserEntity>> logger)
            : base(store, optionAccessor, passwordHasher, userValidators,
                   passwordValidators, keyNormalizer, errors, services, logger)
        { }

        /// <summary>
        /// Получить пользователей
        /// </summary>
        public async Task<IReadOnlyCollection<BoutiqueUserEntity>> GetUsers() =>
            await Users.ToListAsync();
    }
}