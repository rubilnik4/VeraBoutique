using System;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Configuration;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueConsole.Factories.Services;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize;
using BoutiqueDTO.Models.Interfaces.RestClients;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueConsole.Infrastructure.Services.Authorize
{
    /// <summary>
    /// Авторизация в сервисе одежды
    /// </summary>
    public static class BoutiqueAuthorizeService
    {
        /// <summary>
        /// Получить логин и токен
        /// </summary>
        public static async Task<IResultValue<IRestHttpClient>> AuthorizeJwt(IHostConfigurationDomain hostConfiguration,
                                                                             IAuthorizeDomain authorize, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueRestServiceFactory.GetBoutiqueRestClient(hostConfiguration).
            MapAsync(restClient => Authorize(restClient, hostConfiguration, authorize, boutiqueLogger));

        /// <summary>
        /// Авторизироваться с помощью токена
        /// </summary>
        private static async Task<IResultValue<IRestHttpClient>> Authorize(IRestHttpClient restClient, IHostConfigurationDomain hostConfiguration,
                                                                           IAuthorizeDomain authorizeDomain, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueRestServiceFactory.GetAuthorizeRestService(restClient).ToResultValue().
            ResultValueBindOkAsync(service => service.AuthorizeJwt(authorizeDomain).
                                              VoidTaskAsync(token => LogAuthorize(token, service, boutiqueLogger))).
            ResultValueOkTaskAsync(token => BoutiqueRestServiceFactory.GetBoutiqueRestClient(hostConfiguration, token));

       

        /// <summary>
        /// Логгирование авторизации
        /// </summary>
        private static void LogAuthorize(IResultValue<string> resultToken, IAuthorizeRestService service,
                                         IBoutiqueLogger boutiqueLogger) =>
            resultToken.
            ResultValueVoidOkBad(_ => boutiqueLogger.
                                      Void(_ => boutiqueLogger.ShowMessage($"Токен сервиса [{service.GetType().Name}] получен")),
                                 errors => errors.
                                           Void(_ => boutiqueLogger.ShowMessage($"Ошибка авторизации в сервисе [{service.GetType().Name}]")).
                                           Void(_ => boutiqueLogger.ShowErrors(errors)));
    }
}