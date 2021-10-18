using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueConsole.Factories.Services;
using BoutiqueConsole.Infrastructure.Implementations.Services;
using BoutiqueConsole.Models.Enums;
using BoutiqueDTO.Models.Interfaces.RestClients;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueUserManagement.Infrastructure.Implementations.Services
{
    /// <summary>
    /// Удаление пользователей
    /// </summary>
    public static class BoutiqueDeleteUsers
    {
        /// <summary>
        /// Удалить пользователей
        /// </summary>
        public static async Task<IResultError> DeleteUsers(IRestHttpClient httpClient, IBoutiqueLogger boutiqueLogger) =>
            await httpClient.ToResultValue().
            Void(_ => boutiqueLogger.ShowMessage("Удаление пользователей")).
            ResultValueOk(UserRestServiceFactory.GetUserRestService).
            ResultValueBindErrorsOkAsync(restService => restService.DeleteUsers()).
            VoidTaskAsync(result => BoutiqueServiceLogging.LogServiceAction<string, IBoutiqueUserDomain>(result, boutiqueLogger, ServiceActionType.Delete)).
            VoidTaskAsync(_ => boutiqueLogger.ShowMessage("Удаление данных завершено"));
    }
}