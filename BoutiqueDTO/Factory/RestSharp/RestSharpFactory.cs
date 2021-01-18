using System;
using BoutiqueDTO.Models.Interfaces.Connection;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization.Json;
using RestSharp.Serializers.SystemTextJson;

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
             new RestClient(hostConnection.Host)
             {
                 Timeout = GetTimeOut(hostConnection.TimeOut),
             }.UseSystemTextJson();

        /// <summary>
        /// Создать api клиент c jwt токеном
        /// </summary>
        public static IRestClient GetRestClient(IHostConnection hostConnection, string jwtToken) =>
            new RestClient(hostConnection.Host)
            {
                Authenticator = new JwtAuthenticator(jwtToken),
                Timeout = GetTimeOut(hostConnection.TimeOut),
            }.UseSystemTextJson();

        /// <summary>
        /// Получить время в миллисекундах
        /// </summary>
        private static int GetTimeOut(TimeSpan timeOut) =>
            (int)timeOut.TotalMilliseconds;
    }
}