using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using BoutiqueDTO.Models.Interfaces.Base;
using BoutiqueDTO.Models.Interfaces.RestClients;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Interfaces.Result;
using Moq;

namespace BoutiqueDTOXUnit.Infrastructure.Mocks.Services
{
    /// <summary>
    /// Клиент для Api сервисов
    /// </summary>
    public static class RestClientMock
    {
        /// <summary>
        /// Получить клиент для Api сервисов
        /// </summary>
        public static Mock<IRestHttpClient> GetRestClient<TValue>(IResultCollection<TValue> result)
            where TValue : notnull =>
            new Mock<IRestHttpClient>().
            Void(mock => mock.Setup(client => client.GetCollectionAsync<TValue>(It.IsAny<string>())).
                              ReturnsAsync(result));

        /// <summary>
        /// Получить клиент для Api сервисов
        /// </summary>
        public static Mock<IRestHttpClient> GetRestClient<TValue>(IResultValue<TValue> result)
            where TValue : notnull =>
            new Mock<IRestHttpClient>().
            Void(mock => mock.Setup(client => client.GetValueAsync<TValue>(It.IsAny<string>())).
                              ReturnsAsync(result));

        /// <summary>
        /// Получить клиент для Api сервисов
        /// </summary>
        public static Mock<IRestHttpClient> GetRestClient(IResultValue<byte[]> result) =>
            new Mock<IRestHttpClient>().
            Void(mock => mock.Setup(client => client.GetByteAsync(It.IsAny<string>())).
                              ReturnsAsync(result));

        /// <summary>
        /// Получить клиент для Api сервисов
        /// </summary>
        public static Mock<IRestHttpClient> PostRestClient<TValue>(IResultCollection<TValue> result)
            where TValue : notnull =>
            new Mock<IRestHttpClient>().
            Void(mock => mock.Setup(client => client.PostCollectionAsync<TValue>(It.IsAny<string>(), It.IsAny<string>())).
                              ReturnsAsync(result));

        /// <summary>
        /// Получить клиент для Api сервисов
        /// </summary>
        public static Mock<IRestHttpClient> PostRestClient(IResultValue<string> result) =>
            new Mock<IRestHttpClient>().
            Void(mock => mock.Setup(client => client.PostAsync(It.IsAny<string>(), It.IsAny<string>())).
                              ReturnsAsync(result));

        /// <summary>
        /// Получить клиент для Api сервисов
        /// </summary>
        public static Mock<IRestHttpClient> PostRestClient<TValue>(IResultValue<TValue> result)
            where TValue : notnull =>
            new Mock<IRestHttpClient>().
            Void(mock => mock.Setup(client => client.PostValueAsync<TValue>(It.IsAny<string>(), It.IsAny<string>())).
                              ReturnsAsync(result));

        /// <summary>
        /// Получить клиент для Api сервисов
        /// </summary>
        public static Mock<IRestHttpClient> PutRestClient(IResultError result) =>
            new Mock<IRestHttpClient>().
            Void(mock => mock.Setup(client => client.PutValueAsync(It.IsAny<string>(), It.IsAny<string>())).
                              ReturnsAsync(result));

        /// <summary>
        /// Получить клиент для Api сервисов
        /// </summary>
        public static Mock<IRestHttpClient> DeleteRestClient(IResultError result) =>
            new Mock<IRestHttpClient>().
            Void(mock => mock.Setup(client => client.DeleteCollectionAsync(It.IsAny<string>())).
                              ReturnsAsync(result));

        /// <summary>
        /// Получить клиент для Api сервисов
        /// </summary>
        public static Mock<IRestHttpClient> DeleteRestClient<TValue>(IResultValue<TValue> result)
            where TValue : notnull =>
            new Mock<IRestHttpClient>().
            Void(mock => mock.Setup(client => client.DeleteValueAsync<TValue>(It.IsAny<string>())).
                              ReturnsAsync(result));
    }
}