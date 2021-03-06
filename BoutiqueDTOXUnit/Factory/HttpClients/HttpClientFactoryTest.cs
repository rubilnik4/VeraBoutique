﻿using System;
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
            var restClient = HttpClientFactory.GetRestClient(hostConnection);
            
            Assert.Equal(hostConnection.Host, restClient.BaseAddress);
            Assert.Equal(hostConnection.TimeOut, restClient.TimeOut);
            Assert.Equal(AuthorizationType.None, restClient.AuthorizationType);
        }

        /// <summary>
        /// Создать api клиент
        /// </summary>
        [Fact]
        public void GetRestClientAuth_Ok()
        {
            var hostConnection = HostConnection;
            const string jwtToken = "jwtToken";

            var restClient = HttpClientFactory.GetRestClient(hostConnection, jwtToken);

            Assert.Equal(hostConnection.Host, restClient.BaseAddress);
            Assert.Equal(hostConnection.TimeOut, restClient.TimeOut);
            Assert.Equal(AuthorizationType.Bearer, restClient.AuthorizationType);
            Assert.Equal(jwtToken, restClient.JwtToken);
        }

        /// <summary>
        /// Параметры подключения
        /// </summary>
        private static IHostConfigurationDomain HostConnection =>
            new HostConfigurationDomain(new Uri("https://localhost:5001/"), TimeSpan.FromSeconds(5), false);
    }
}