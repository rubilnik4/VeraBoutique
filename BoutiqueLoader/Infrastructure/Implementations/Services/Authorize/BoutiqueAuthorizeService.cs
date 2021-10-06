using System;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize;
using BoutiqueDTO.Models.Interfaces.RestClients;
using BoutiqueLoader.Factories.Configuration;
using BoutiqueLoader.Factories.Services;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueLoader.Infrastructure.Implementations.Services.Authorize
{
    /// <summary>
    /// Авторизация в сервисе одежды
    /// </summary>
    public static class BoutiqueAuthorizeService
    {
        /// <summary>
        /// Получить логин и токен
        /// </summary>
        public static async Task<IResultValue<string>> AuthorizeJwt(IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueAuthorizeFunc(boutiqueLogger).
            ResultValueCurryOkAsync(BoutiqueRestServiceFactory.GetBoutiqueRestClient(boutiqueLogger)).
            ResultValueCurryOkBindAsync(AuthorizeConfigurationFactory.GetConfiguration(boutiqueLogger)).
            ResultValueBindOkBindAsync(func => func());

        /// <summary>
        /// Функция авторизации
        /// </summary>
        private static IResultValue<Func<IRestHttpClient, IAuthorizeDomain, Task<IResultValue<string>>>> BoutiqueAuthorizeFunc(IBoutiqueLogger boutiqueLogger) =>
            new ResultValue<Func<IRestHttpClient, IAuthorizeDomain, Task<IResultValue<string>>>>((restClient, authorizeDomain) =>
                Authorize(restClient, authorizeDomain, boutiqueLogger));

        /// <summary>
        /// Авторизироваться с помощью токена
        /// </summary>
        private static async Task<IResultValue<string>> Authorize(IRestHttpClient restClient, IAuthorizeDomain authorizeDomain,
                                                                  IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueRestServiceFactory.GetAuthorizeRestService(restClient).ToResultValue().
            ResultValueBindOkAsync(service => service.AuthorizeJwt(authorizeDomain).
                                              VoidTaskAsync(token => LogAuthorize(token, service, boutiqueLogger)));

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