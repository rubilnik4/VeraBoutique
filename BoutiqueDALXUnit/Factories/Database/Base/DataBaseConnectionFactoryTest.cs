using System;
using System.Linq;
using BoutiqueDAL.Factories.Implementations.Database.Connection;
using BoutiqueDAL.Models.Implementations.Connection;
using BoutiqueDALXUnit.Data;
using BoutiqueDALXUnit.Data.Database.Implementation;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.DatabaseErrors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.CommonErrors;
using Xunit;

namespace BoutiqueDALXUnit.Factories.Database.Base
{
    /// <summary>
    /// Фабрика создания подключения базы данных. Тесты
    /// </summary>
    public class DataBaseConnectionFactoryTest
    {
        /// <summary>
        /// Получить параметры подключения. Верные параметры
        /// </summary>
        [Fact]
        public void GetHostConnection_Ok()
        {
            const string host = "localhost";
            const string port = "5032";

            var hostConnectionResult = DatabaseConnectionFactory.GetHostConnection(host, port);

            var hostConnectionAssert = new HostConnection(host, Int32.Parse(port));
            Assert.True(hostConnectionResult.OkStatus);
            Assert.True(hostConnectionAssert.Equals(hostConnectionResult.Value));
        }

        /// <summary>
        /// Получить параметры подключения к базе из переменных окружения. Некорректное имя сервера
        /// </summary>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GetHostConnection_BadHost(string host)
        {
            const string port = "5032";

            var hostConnectionResult = DatabaseConnectionFactory.GetHostConnection(host, port);

            Assert.True(hostConnectionResult.HasErrors);
            Assert.IsType<DatabaseConnectionErrorResult>(hostConnectionResult.Errors.First());
        }

        /// <summary>
        /// Получить параметры подключения к базе из переменных окружения. Некорректный порт
        /// </summary>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("port")]
        [InlineData("0")]
        [InlineData("-1")]
        public void GetHostConnection_BadPort(string port)
        {
            const string host = "localhost";

            var hostConnectionResult = DatabaseConnectionFactory.GetHostConnection(host, port);

            Assert.True(hostConnectionResult.HasErrors);
            Assert.IsType<DatabaseConnectionErrorResult>(hostConnectionResult.Errors.First());
        }

        /// <summary>
        /// Получить параметры аутентификации. Верные параметры
        /// </summary>
        [Fact]
        public void GetAuthorization_Ok()
        {
            const string username = "username";
            const string password = "password";

            var authorizationResult = DatabaseConnectionFactory.GetAuthorization(username, password);

            var authorizationAssert = new Authorization(username, password);
            Assert.True(authorizationResult.OkStatus);
            Assert.True(authorizationAssert.Equals(authorizationResult.Value));
        }

        /// <summary>
        /// Получить параметры аутентификации. Некорректное имя пользователя
        /// </summary>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GetAuthorization_BadUsername(string username)
        {
            const string password = "password";

            var authorizationResult = DatabaseConnectionFactory.GetAuthorization(username, password);

            Assert.True(authorizationResult.HasErrors);
            Assert.IsType<DatabaseConnectionErrorResult>(authorizationResult.Errors.First());
        }

        /// <summary>
        /// Получить параметры аутентификации. Некорректный пароль
        /// </summary>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GetAuthorization_BadPassword(string password)
        {
            const string username = "username";

            var authorizationResult = DatabaseConnectionFactory.GetAuthorization(username, password);

            Assert.True(authorizationResult.HasErrors);
            Assert.IsType<DatabaseConnectionErrorResult>(authorizationResult.Errors.First());
        }

        /// <summary>
        /// Получить параметры подключения к базе. Верные параметры
        /// </summary>
        [Fact]
        public void GetDatabaseConfiguration_Ok()
        {
            var databaseConnection = DatabaseConnectionFactory.GetDatabaseConnection(TestConnectionData.HostConnection.Host,
                                                                                     TestConnectionData.HostConnection.Port.ToString(),
                                                                                     TestConnectionData.Database,
                                                                                     TestConnectionData.Authorization.Username,
                                                                                     TestConnectionData.Authorization.Password);

            Assert.True(databaseConnection.OkStatus);
            Assert.True(TestConnectionData.DatabaseConnection.Equals(databaseConnection.Value));
        }

        /// <summary>
        /// Получить параметры подключения к базе. Некорректный хост параметры
        /// </summary>
        [Fact]
        public void GetDatabaseConfiguration_BadHost()
        {
            var databaseConnection = DatabaseConnectionFactory.GetDatabaseConnection(String.Empty,
                                                                                     TestConnectionData.HostConnection.Port.ToString(),
                                                                                     TestConnectionData.Database,
                                                                                     TestConnectionData.Authorization.Username,
                                                                                     TestConnectionData.Authorization.Password);

            Assert.True(databaseConnection.HasErrors);
            Assert.IsType<DatabaseConnectionErrorResult>(databaseConnection.Errors.First());
        }

        /// <summary>
        /// Получить параметры подключения к базе. Некорректные параметры авторизации
        /// </summary>
        [Fact]
        public void GetDatabaseConfiguration_BadAuthorization()
        {
            var databaseConnection = DatabaseConnectionFactory.GetDatabaseConnection(TestConnectionData.HostConnection.Host,
                                                                                     TestConnectionData.HostConnection.Port.ToString(),
                                                                                     String.Empty, 
                                                                                     TestConnectionData.Authorization.Username,
                                                                                     TestConnectionData.Authorization.Password);

            Assert.True(databaseConnection.HasErrors);
            Assert.IsType<DatabaseConnectionErrorResult>(databaseConnection.Errors.First());
        }

        /// <summary>
        /// Получить параметры подключения к базе. Некорректная база данных
        /// </summary>
        [Fact]
        public void GetDatabaseConfiguration_BadDatabase()
        {
            var databaseConnection = DatabaseConnectionFactory.GetDatabaseConnection(TestConnectionData.HostConnection.Host,
                                                                                     TestConnectionData.HostConnection.Port.ToString(),
                                                                                     TestConnectionData.Database,
                                                                                     String.Empty, 
                                                                                     TestConnectionData.Authorization.Password);

            Assert.True(databaseConnection.HasErrors);
            Assert.IsType<DatabaseConnectionErrorResult>(databaseConnection.Errors.First());
        }
    }
}