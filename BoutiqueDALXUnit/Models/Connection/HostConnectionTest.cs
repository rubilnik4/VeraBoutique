using System;
using BoutiqueDAL.Models.Implementations.Connection;
using Xunit;

namespace BoutiqueDALXUnit.Models.Connection
{
    /// <summary>
    /// Параметры подключения. Тесты
    /// </summary>
    public class HostConnectionTest
    {
        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void Equal_Ok()
        {
            const string host = "localhost";
            const int port = 5432;

            var hostConnection = new HostConnection(host, port);

            int hostConnectionHash = HashCode.Combine(host, port);
            Assert.Equal(hostConnectionHash, hostConnection.GetHashCode());
        }
    }
}