using System.Threading.Tasks;
using BoutiquePrerequisites.Factories.Connection;
using BoutiquePrerequisites.Factories.DatabaseInitialize.Boutique;
using BoutiquePrerequisites.Factories.Services;
using BoutiquePrerequisites.Infrastructure.Interfaces.Logger;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Interfaces.Result;
using RestSharp;

namespace BoutiquePrerequisites.Infrastructure.Implementations.BoutiqueDatabase.Services.Authorization
{
    /// <summary>
    /// Авторизация в сервисе одежды
    /// </summary>
    public static class BoutiqueAuthorizeService
    {
        public static async Task<IResultValue<string>> AuthorizeJwt(ILogger logger) =>
            await BoutiqueRestServiceFactory.GetBoutiqueRestClient().
            ResultValueBindOkAsync(restClient => Authorize(restClient, logger));

          /// <summary>
          /// Загрузить тип пола в базу
          /// </summary>
        private static async Task<IResultValue<string>> Authorize(IRestClient restClient, ILogger logger) =>
            await BoutiqueAuthorize.BoutiqueLogin.
            ResultValueBindOkAsync(authorizeDomain => BoutiqueRestServiceFactory.GetAuthorizeRestService(restClient, logger).
                                   AuthorizeJwt(authorizeDomain));
    }
}