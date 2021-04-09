using System;
using System.Net.Http;
using System.Net.Http.Headers;
using BoutiqueCommon.Models.Domain.Interfaces.Configuration;
using Functional.FunctionalExtensions.Sync;

namespace BoutiqueDTO.Factory.RestSharp
{
    /// <summary>
    /// Фабрика создания api клиента
    /// </summary>
    public static class HttpClientFactory
    {
        /// <summary>
        /// Схема работы авторизации jwt
        /// </summary>
        public const string JWT_SCHEME = "Bearer";

        /// <summary>
        /// Создать api клиент
        /// </summary>
        public static HttpClient GetRestClient(IHostConfigurationDomain hostConfiguration) =>
            GetRestClient(hostConfiguration, String.Empty);

        /// <summary>
        /// Создать api клиент c jwt токеном
        /// </summary>
        public static HttpClient GetRestClient(IHostConfigurationDomain hostConfiguration, string jwtToken) =>
            new HttpClientHandler().
            VoidOk(handler => hostConfiguration.DisableSSL,
                   handler => handler.ClientCertificateOptions = ClientCertificateOption.Manual).
            VoidOk(handler => hostConfiguration.DisableSSL,
                   handler => handler.ServerCertificateCustomValidationCallback =
                       (httpRequestMessage, cert, cetChain, policyErrors) => true).
            Map(handler => new HttpClient(null)
            {
                BaseAddress = hostConfiguration.Host,
                Timeout = hostConfiguration.TimeOut,
            }).
            VoidOk(_ => !String.IsNullOrWhiteSpace(jwtToken),
                   httpClient => httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JWT_SCHEME, jwtToken));
    }
}