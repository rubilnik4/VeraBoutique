﻿using System;
using BoutiqueCommon.Models.Domain.Implementations.Configuration;
using BoutiqueCommon.Models.Domain.Interfaces.Configuration;
using BoutiqueDTO.Factory.RestSharp;
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
            var restClient = HttpClientFactory.GetRestClient(hostConnection);
            
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

            var restClient = HttpClientFactory.GetRestClient(hostConnection, jwtToken);

            Assert.Equal(hostConnection.Host, restClient.BaseAddress);
            Assert.Equal(hostConnection.TimeOut, restClient.Timeout);
            Assert.Equal(HttpClientFactory.JWT_SCHEME, restClient.DefaultRequestHeaders.Authorization?.Scheme);
            Assert.Equal(jwtToken, restClient.DefaultRequestHeaders.Authorization?.Parameter);
        }

        /// <summary>
        /// Параметры подключения
        /// </summary>
        private static IHostConfigurationDomain HostConnection =>
            new HostConfigurationDomain(new Uri("https://localhost:5001/"), TimeSpan.FromSeconds(5), false);
    }
}