using System;
using BoutiqueDTO.Factory.RestSharp;
using BoutiqueDTO.Models.Implementations.Connection;
using BoutiqueDTO.Models.Interfaces.Connection;
using RestSharp.Authenticators;
using Xunit;

namespace BoutiqueDTOXUnit.Factory.RestSharp
{
    /// <summary>
    /// Фабрика создания api клиента. Тесты
    /// </summary>
    public class RestSharpFactoryTest
    {
        /// <summary>
        /// Создать api клиент
        /// </summary>
        [Fact]
        public void GetRestClient_Ok()
        {
            var hostConnection = HostConnection;
            var restClient = RestSharpFactory.GetRestClient(hostConnection);
            
            Assert.Equal(hostConnection.Host, restClient.BaseUrl);
            Assert.Equal(hostConnection.TimeOut.TotalMilliseconds, restClient.Timeout);
        }

        /// <summary>
        /// Создать api клиент
        /// </summary>
        [Fact]
        public void GetRestClientAuth_Ok()
        {
            var hostConnection = HostConnection;
            const string jwtToken = "jwtToken";

            var restClient = RestSharpFactory.GetRestClient(hostConnection, jwtToken);
            var authenticator = (JwtAuthenticator)restClient.Authenticator;

            Assert.Equal(hostConnection.Host, restClient.BaseUrl);
            Assert.Equal(hostConnection.TimeOut.TotalMilliseconds, restClient.Timeout);
            Assert.IsType<JwtAuthenticator>(restClient.Authenticator);
        }

        /// <summary>
        /// Параметры подключения
        /// </summary>
        private static IHostConnection HostConnection =>
            new HostConnection(new Uri("https://localhost:5001/"), TimeSpan.FromSeconds(5));
    }
}