using System;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueLoader.Factories.Connection;
using BoutiqueLoader.Factories.Services;
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
        public static async Task<IResultValue<string>> AuthorizeJwt(IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueAuthorizeFunc(boutiqueLogger).
            ResultCurryOkBind(BoutiqueRestServiceFactory.GetBoutiqueRestClient()).
            ResultCurryOkBind(BoutiqueAuthorize.BoutiqueLogin).
            ResultValueBindOkAsync(func => func());

        /// <summary>
        /// Функция авторизации
        /// </summary>
        private static IResultValue<Func<IRestClient, IAuthorizeDomain, Task<IResultValue<string>>>> BoutiqueAuthorizeFunc(IBoutiqueLogger boutiqueLogger) =>
            new ResultValue<Func<IRestClient, IAuthorizeDomain, Task<IResultValue<string>>>>((restClient, authorizeDomain) =>
                Authorize(restClient, authorizeDomain, boutiqueLogger));

        /// <summary>
        /// Загрузить тип пола в базу
        /// </summary>
        private static async Task<IResultValue<string>> Authorize(IRestClient restClient, IAuthorizeDomain authorizeDomain,
                                                                  IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueRestServiceFactory.GetAuthorizeRestService(restClient).ToResultValue().
            ResultValueBindOkAsync(service =>
                service.AuthorizeJwt(authorizeDomain).
                ResultValueVoidOkBadTaskAsync(_ => boutiqueLogger.Void(_ => boutiqueLogger.ShowMessage($"Токен сервиса [{service.GetType().Name}] получен")),
                                                   errors => errors.
                                                   Void(_ => boutiqueLogger.ShowMessage($"Ошибка авторизации в сервисе [{service.GetType().Name}]")).
                                                   Void(_ => boutiqueLogger.ShowErrors(errors))));

        private static void LogAuthorize()
    }
}