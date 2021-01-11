using System;
using BoutiqueDTO.Models.Interfaces.Connection;
using RestSharp;
using RestSharp.Authenticators;

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
                 Timeout = (int)hostConnection.TimeOut.TotalMilliseconds,
             };
    }
}