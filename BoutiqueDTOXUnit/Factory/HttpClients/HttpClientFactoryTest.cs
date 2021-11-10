using System;
using System.Net.Http;
using BoutiqueCommon.Models.Domain.Implementations.Configuration;
using BoutiqueCommon.Models.Domain.Interfaces.Configuration;
using BoutiqueDTO.Factory.HttpClients;
using BoutiqueDTO.Models.Enums.RestClients;
using Xunit;

namespace BoutiqueDTOXUnit.Factory.HttpClients
{
    /// <summary>
    /// Фабрика создания api клиента. Тесты
    /// </summary>
    public class HttpClientFactoryTest
    {
        /// <summary>
        /// Создать api клиент
        /// </summary>
        [Fact]
        public void GetRestClient_Ok()
        {
            var hostConnection = HostConnection;
            var restClient = HttpClientFactory.GetRestClient(HttpClientHandler, hostConnection.Host, hostConnection.TimeOut);
            
            Assert.Equal(hostConnection.Host, restClient.BaseAddress);
            Assert.Equal(hostConnection.TimeOut, restClient.Timeout);
        }

        /// <summary>
        /// Создать api клиент
        /// </summary>
        [Fact]
        public void GetRestClientAuth_Ok()
        {
            var hostConnection = HostConnection;
            const string jwtToken = "jwtToken";

            var restClient = HttpClientFactory.GetRestClient(HttpClientHandler, hostConnection.Host, hostConnection.TimeOut, jwtToken);

            Assert.Equal(hostConnection.Host, restClient.BaseAddress);
            Assert.Equal(hostConnection.TimeOut, restClient.Timeout);
            Assert.Equal(HttpClientSchemaType.Bearer.ToString(), restClient.DefaultRequestHeaders.Authorization?.Scheme);
            Assert.Equal(jwtToken, restClient.DefaultRequestHeaders.Authorization?.Parameter);
        }

        /// <summary>
        /// Параметры подключения
        /// </summary>
        private static IHostConfigurationDomain HostConnection =>
            new HostConfigurationDomain(new Uri("https://localhost:5001/"), TimeSpan.FromSeconds(5));

        /// <summary>
        /// Обработчик запросов
        /// </summary>
        private static HttpClientHandler HttpClientHandler =>
            new();
    }
}