using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.TaskExtensions;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Identities;
using BoutiqueDAL.Models.Implementations.Identities;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData.Identities
{
    /// <summary>
    /// Инициализация пользователей
    /// </summary>
    public static class IdentityUsersInitialize
    {
        /// <summary>
        /// Проверить и добавить пользователей
        /// </summary>
        public static async Task<IResultCollection<string>> CreateIdentityUsers(IUserManagerService userManager, 
                                                                                IEnumerable<BoutiqueRoleUser> defaultUsers) =>
            await defaultUsers.
            Select(user => userManager.CreateRoleUser(user.BoutiqueIdentityUser, user.IdentityRoleType)).
            WaitAllInLine().
            ToResultCollectionTaskAsync();
    }
}