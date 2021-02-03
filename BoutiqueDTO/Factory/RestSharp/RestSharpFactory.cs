using System;
using BoutiqueCommon.Models.Domain.Interfaces.Configuration;
using Functional.FunctionalExtensions.Sync;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization.Json;
using RestSharp.Serializers.NewtonsoftJson;

namespace BoutiqueDTO.Factory.RestSharp
{
    /// <summary>
    /// Фабрика создания api клиента
    /// </summary>
    public static class RestSharpFactory
    {
        /// <summary>
        /// Создать api клиент
        /// </summary>
        public static IRestClient GetRestClient(IHostConfigurationDomain hostConfiguration) =>
            GetRestClient(hostConfiguration, String.Empty);

        /// <summary>
        /// Создать api клиент c jwt токеном
        /// </summary>
        public static IRestClient GetRestClient(IHostConfigurationDomain hostConfiguration, string jwtToken) =>
            new RestClient(hostConfiguration.Host)
            {
                Authenticator = String.IsNullOrWhiteSpace(jwtToken) ? null : new JwtAuthenticator(jwtToken),
                Timeout = GetTimeOut(hostConfiguration.TimeOut),
            }.UseNewtonsoftJson().
            VoidOk(_ => hostConfiguration.DisableSSL,
                        restClient => restClient.RemoteCertificateValidationCallback =
                            (sender, certificate, chain, sslPolicyErrors) => true);

        /// <summary>
        /// Получить время в миллисекундах
        /// </summary>
        private static int GetTimeOut(TimeSpan timeOut) =>
            (int)timeOut.TotalMilliseconds;
    }
}