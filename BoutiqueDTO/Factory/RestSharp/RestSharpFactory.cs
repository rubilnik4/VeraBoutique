using System;
using BoutiqueDTO.Models.Interfaces.Connection;
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
        public static IRestClient GetRestClient(IHostConnection hostConnection) =>
            GetRestClient(hostConnection, String.Empty);

        /// <summary>
        /// Создать api клиент c jwt токеном
        /// </summary>
        public static IRestClient GetRestClient(IHostConnection hostConnection, string jwtToken) =>
            new RestClient(hostConnection.Host)
            {
                Authenticator = String.IsNullOrWhiteSpace(jwtToken) ? null : new JwtAuthenticator(jwtToken),
                Timeout = GetTimeOut(hostConnection.TimeOut),
            }.UseNewtonsoftJson().
            VoidOk(_ => hostConnection.DisableSSLValidation,
                      restClient => restClient.RemoteCertificateValidationCallback =
                          (sender, certificate, chain, sslPolicyErrors) => true);

        /// <summary>
        /// Получить время в миллисекундах
        /// </summary>
        private static int GetTimeOut(TimeSpan timeOut) =>
            (int)timeOut.TotalMilliseconds;
    }
}