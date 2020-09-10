using System;
using BoutiqueDAL.Models.Implementations.Connection;
using BoutiqueDALXUnit.Data;
using BoutiqueDALXUnit.Data.Database.Implementation;
using Xunit;

namespace BoutiqueDALXUnit.Models.Connection
{
    /// <summary>
    /// Параметры подключения к базе данных. Тесты
    /// </summary>
    public class DatabaseConnectionTest
    {
        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void Equal_Ok()
        {
            var hostConnection = TestConnectionData.HostConnectionOk;
            var authorization = TestConnectionData.AuthorizationOk;
            string database = TestConnectionData.DatabaseOk;

            var databaseConnection = new DatabaseConnection(hostConnection, database, authorization);

            int databaseConnectionHash = HashCode.Combine(hostConnection, database, authorization);
            Assert.Equal(databaseConnectionHash, databaseConnection.GetHashCode());
        }
    }
}