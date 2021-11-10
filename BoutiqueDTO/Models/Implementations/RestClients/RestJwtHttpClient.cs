using System;
using System.Net.Http;
using System.Threading.Tasks;
using BoutiqueDTO.Factory.HttpClients;
using BoutiqueDTO.Models.Interfaces.RestClients;
using ResultFunctional.FunctionalExtensions.Async;

namespace BoutiqueDTO.Models.Implementations.RestClients
{
    /// <summary>
    /// Клиент для http запросов с jwt авторизацией
    /// </summary>
    public class RestJwtHttpClient : RestHttpClient, IRestJwtHttpClient
    {
        public RestJwtHttpClient(HttpClientHandler httpClientHandler, Uri baseAddress, TimeSpan timeOut, Func<Task<string?>> tokenFunc)
            : base(httpClientHandler, baseAddress, timeOut)
        {
            _tokenFunc = tokenFunc;
        }

        /// <summary>
        /// Токен
        /// </summary>
        private readonly Func<Task<string?>> _tokenFunc;

        /// <summary>
        /// Клиент для http запросов
        /// </summary>
        protected override async Task<HttpClient> GetHttpClient() =>
            await _tokenFunc().
            MapTaskAsync(token => HttpClientFactory.GetRestClient(HttpClientHandler, BaseAddress, Timeout, token));
    }
}