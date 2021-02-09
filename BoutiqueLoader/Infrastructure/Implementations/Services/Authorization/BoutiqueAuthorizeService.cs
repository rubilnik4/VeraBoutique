using System;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorization;
using BoutiqueLoader.Factories.Configuration;
using BoutiqueLoader.Factories.Services;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using RestSharp;

namespace BoutiqueLoader.Infrastructure.Implementations.Services.Authorization
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
            ResultCurryBindOkAsync(BoutiqueRestServiceFactory.GetBoutiqueRestClient(boutiqueLogger)).
            ResultCurryBindOkBindAsync(AuthorizeConfigurationFactory.GetConfiguration(boutiqueLogger)).
            ResultValueBindOkBindAsync(func => func());

        /// <summary>
        /// Функция авторизации
        /// </summary>
        private static IResultValue<Func<IRestClient, IAuthorizeDomain, Task<IResultValue<string>>>> BoutiqueAuthorizeFunc(IBoutiqueLogger boutiqueLogger) =>
            new ResultValue<Func<IRestClient, IAuthorizeDomain, Task<IResultValue<string>>>>((restClient, authorizeDomain) =>
                Authorize(restClient, authorizeDomain, boutiqueLogger));

        /// <summary>
        /// Авторизироваться с помощью токена
        /// </summary>
        private static async Task<IResultValue<string>> Authorize(IRestClient restClient, IAuthorizeDomain authorizeDomain,
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