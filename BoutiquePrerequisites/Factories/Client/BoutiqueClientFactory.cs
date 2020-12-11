using System;
using System.Net.Http;
using BoutiquePrerequisites.Factories.Connection;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Interfaces.Result;

namespace BoutiquePrerequisites.Factories.Client
{
    /// <summary>
    /// Клиент для подключения к серверу одежды
    /// </summary>
    public static class BoutiqueClientFactory
    {
        /// <summary>
        /// Клиент для подключения к серверу одежды
        /// </summary>
        public static IResultValue<Boutique.Client> BoutiqueClient =>
            BoutiqueConnection.BoutiqueHostConnection.
            ResultValueOk(hostConnection => new HttpClient { Timeout = TimeSpan.FromSeconds(hostConnection.TimeOut) }.
                                            Map(httpClient => new Boutique.Client(httpClient)));
    }
}