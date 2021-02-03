using System.Threading.Tasks;
using BoutiqueDTO.Extensions.RestResponses.Async;
using BoutiqueDTO.Factory.RestSharp;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Base;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Authorization;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Identity;
using Functional.Models.Interfaces.Result;
using RestSharp;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.Api.Authorization
{
    /// <summary>
    /// Api сервис авторизации
    /// </summary>
    public class AuthorizeApiService : ApiServiceBase<(string, string), AuthorizeTransfer>, IAuthorizeApiService
    {
        public AuthorizeApiService(IRestClient restClient)
            : base(restClient)
        { }

        /// <summary>
        /// Авторизация через токен
        /// </summary>
        public async Task<IResultValue<string>> AuthorizeJwt(AuthorizeTransfer authorizeTransfer) =>
            await RestClient.ExecuteAsync<string>(ApiRestRequest.PostJsonRequest<(string, string), AuthorizeTransfer>(authorizeTransfer, ControllerName)).
            ToRestResultValueAsync();
    }
}