using System;
using System.Linq;
using BoutiqueDAL.Factories.Implementations;
using BoutiqueDAL.Models.Implementations.Connection;
using BoutiqueDALXUnit.Data;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Xunit;

namespace BoutiqueDALXUnit.Factories
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
            Assert.True(hostConnectionResult.Errors.First().ErrorResultType == ErrorResultType.IncorrectDatabaseConnection);
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
            Assert.True(hostConnectionResult.Errors.First().ErrorResultType == ErrorResultType.IncorrectDatabaseConnection);
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
            Assert.True(authorizationResult.Errors.First().ErrorResultType == ErrorResultType.IncorrectDatabaseConnection);
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
            Assert.True(authorizationResult.Errors.First().ErrorResultType == ErrorResultType.IncorrectDatabaseConnection);
        }

        /// <summary>
        /// Получить параметры подключения к базе. Верные параметры
        /// </summary>
        [Fact]
        public void GetDatabaseConfiguration_Ok()
        {
            var hostConnection = new ResultValue<HostConnection>(ConnectionData.HostConnectionOk);
            var authorization = new ResultValue<Authorization>(ConnectionData.AuthorizationOk);
            var database = new ResultValue<string>(ConnectionData.DatabaseOk);

            var databaseConnection = DatabaseConnectionFactory.GetDatabaseConnection(hostConnection, database, authorization);

            var databaseConnectionAssert = new DatabaseConnection(hostConnection.Value, database.Value, authorization.Value);
            Assert.True(databaseConnection.OkStatus);
            Assert.True(databaseConnectionAssert.Equals(databaseConnection.Value));
        }

        /// <summary>
        /// Получить параметры подключения к базе. Некорректный хост параметры
        /// </summary>
        [Fact]
        public void GetDatabaseConfiguration_BadHost()
        {
            var hostConnection = new ResultValue<HostConnection>(ConnectionData.ErrorConnection);
            var authorization = new ResultValue<Authorization>(ConnectionData.AuthorizationOk);
            var database = new ResultValue<string>(ConnectionData.DatabaseOk);

            var databaseConnection = DatabaseConnectionFactory.GetDatabaseConnection(hostConnection, database, authorization);

            Assert.True(databaseConnection.HasErrors);
            Assert.True(databaseConnection.Errors.First().ErrorResultType == ErrorResultType.IncorrectDatabaseConnection);
        }

        /// <summary>
        /// Получить параметры подключения к базе. Некорректные параметры авторизации
        /// </summary>
        [Fact]
        public void GetDatabaseConfiguration_BadAuthorization()
        {
            var hostConnection = new ResultValue<HostConnection>(ConnectionData.HostConnectionOk);
            var authorization = new ResultValue<Authorization>(ConnectionData.ErrorConnection);
            var database = new ResultValue<string>(ConnectionData.DatabaseOk);

            var databaseConnection = DatabaseConnectionFactory.GetDatabaseConnection(hostConnection, database, authorization);

            Assert.True(databaseConnection.HasErrors);
            Assert.True(databaseConnection.Errors.First().ErrorResultType == ErrorResultType.IncorrectDatabaseConnection);
        }

        /// <summary>
        /// Получить параметры подключения к базе. Некорректная база данных
        /// </summary>
        [Fact]
        public void GetDatabaseConfiguration_BadDatabase()
        {
            var hostConnection = new ResultValue<HostConnection>(ConnectionData.HostConnectionOk);
            var authorization = new ResultValue<Authorization>(ConnectionData.AuthorizationOk);
            var database = new ResultValue<string>(ConnectionData.ErrorConnection);

            var databaseConnection = DatabaseConnectionFactory.GetDatabaseConnection(hostConnection, database, authorization);

            Assert.True(databaseConnection.HasErrors);
            Assert.True(databaseConnection.Errors.First().ErrorResultType == ErrorResultType.IncorrectDatabaseConnection);
        }
    }
}