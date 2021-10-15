using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueConsole.Factories.Services;
using BoutiqueDTO.Models.Interfaces.RestClients;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueUserManagement.Infrastructure.Implementations.Services
{
    /// <summary>
    /// Получение пользователей
    /// </summary>
    public static class BoutiqueGetUsers
    {
        /// <summary>
        /// Получить пользователей
        /// </summary>
        public static async Task<IResultError> GetUsers(IRestHttpClient httpClient, IBoutiqueLogger boutiqueLogger) =>
            await httpClient.ToResultValue().
            Void(_ => boutiqueLogger.ShowMessage("Получение пользователей")).
            ResultValueOk(UserRestServiceFactory.GetUserRestService).
            ResultValueBindOkToCollectionAsync(restService => restService.GetUsers()).
            ResultCollectionVoidOkTaskAsync(users => PrintUserName(users, boutiqueLogger)).
            VoidTaskAsync(_ => boutiqueLogger.ShowMessage("Получение данных завершено"));

        /// <summary>
        /// Отобразить имена пользователей
        /// </summary>
        private static void PrintUserName(IEnumerable<IBoutiqueUserDomain> users, IBoutiqueLogger boutiqueLogger)
        {
            foreach (var user in users) boutiqueLogger.ShowMessage($"[{user.IdentityRoleType}] {user.Email}");
        }
    }
}