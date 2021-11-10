using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Configuration;
using BoutiqueDTO.Models.Enums.RestClients;
using BoutiqueDTO.Models.Implementations.RestClients;
using BoutiqueDTO.Models.Interfaces.RestClients;
using ResultFunctional.FunctionalExtensions.Sync;

namespace BoutiqueDTO.Factory.HttpClients
{
    /// <summary>
    /// Фабрика создания api клиента
    /// </summary>
    public static class HttpClientFactory
    {
        /// <summary>
        /// Создать api клиент
        /// </summary>
        public static HttpClient GetRestClient(HttpClientHandler httpClientHandler, Uri baseAddress, TimeSpan timeOut) =>
            GetRestClient(httpClientHandler, baseAddress, timeOut, null);

        /// <summary>
        /// Создать api клиент c jwt токеном
        /// </summary>
        public static HttpClient GetRestClient(HttpClientHandler httpClientHandler, Uri baseAddress, TimeSpan timeOut, string? jwtToken) =>
            new HttpClient(httpClientHandler)
            {
                BaseAddress = baseAddress,
                Timeout = timeOut,
            }.
            VoidOk(_ => !String.IsNullOrWhiteSpace(jwtToken),
                   httpClient => httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(HttpClientSchemaType.Bearer.ToString(), 
                                                                                                                jwtToken));
    }
}