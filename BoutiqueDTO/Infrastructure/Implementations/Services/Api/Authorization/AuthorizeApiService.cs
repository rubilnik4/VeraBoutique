using System.Threading.Tasks;
using BoutiqueDTO.Extensions.RestResponses.Async;
using BoutiqueDTO.Factory.RestSharp;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Base;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Authorization;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Identity;
using BoutiqueDTO.Models.Interfaces.Connection;
using Functional.Models.Interfaces.Result;
using RestSharp;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.Api.Authorization
{
    public class AuthorizeApiService : ApiServiceBase<(string, string), AuthorizeTransfer>, IAuthorizeApiService
    {
        /// <summary>
        /// Api сервис авторизации
        /// </summary>
        public AuthorizeApiService(IRestClient restClient)
            : base(restClient)
        { }

        /// <summary>
        /// Получение данных по идентификатору
        /// </summary>
        public async Task<IResultValue<string>> AuthorizeJwt(AuthorizeTransfer authorizeTransfer) =>
            await RestClient.ExecuteAsync<string>(ApiRestRequest.PostJsonRequest<(string, string), AuthorizeTransfer>(authorizeTransfer, ControllerName)).
            ToRestResultValueAsync();

        /// <summary>
        /// Получить сервис
        /// </summary>
        public static IAuthorizeApiService GetAuthorizeApiService(IHostConnection hostConnection) =>
            new AuthorizeApiService(RestSharpFactory.GetRestClient(hostConnection));
    }
}