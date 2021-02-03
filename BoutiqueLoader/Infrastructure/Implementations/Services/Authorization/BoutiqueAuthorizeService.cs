using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueLoader.Factories.Connection;
using BoutiqueLoader.Factories.Services;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
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
            await BoutiqueRestServiceFactory.GetBoutiqueRestClient().
            ResultValueBindOkAsync(restClient => Authorize(restClient, boutiqueLogger));

          /// <summary>
          /// Загрузить тип пола в базу
          /// </summary>
        private static async Task<IResultValue<string>> Authorize(IRestClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueAuthorize.BoutiqueLogin.
            ResultValueBindOkAsync(authorizeDomain => BoutiqueRestServiceFactory.GetAuthorizeRestService(restClient, boutiqueLogger).
                                   AuthorizeJwt(authorizeDomain));
    }
}