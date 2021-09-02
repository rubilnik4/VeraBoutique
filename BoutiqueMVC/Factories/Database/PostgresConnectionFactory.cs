using System;
using BoutiqueDAL.Factories.Implementations.Database.Connection;
using BoutiqueDAL.Models.Implementations.Connection;
using BoutiqueMVC.Models.Implementations.Environment;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueMVC.Factories.Database
{
    /// <summary>
    /// Параметры подключения базы Postgres
    /// </summary>
    public static class PostgresConnectionFactory
    {
        /// <summary>
        /// Получить параметры подключения к базе из переменных окружения
        /// </summary>
        public static IResultValue<DatabaseConnection> PostgresConnection =>
            DatabaseConnectionFactory.GetDatabaseConnection(DatabaseConnectionFactory.GetHostConnection(Host, Port),
                                                            DatabaseConnectionFactory.GetDatabase(Database),
                                                            DatabaseConnectionFactory.GetAuthorization(Username, Password));
    
        /// <summary>
        /// Имя сервера
        /// </summary>
        private static string? Host =>
            Environment.GetEnvironmentVariable(PostgresEnvironment.HOST_NAME);

        /// <summary>
        /// Порт
        /// </summary>
        private static string? Port =>
            Environment.GetEnvironmentVariable(PostgresEnvironment.PORT_NAME);

        /// <summary>
        /// Имя базы данных
        /// </summary>
        private static string? Database =>
            Environment.GetEnvironmentVariable(PostgresEnvironment.DATABASE_NAME);

        /// <summary>
        /// Имя пользователя
        /// </summary>
        private static string? Username =>
            Environment.GetEnvironmentVariable(PostgresEnvironment.USER_NAME);

        /// <summary>
        /// Пароль
        /// </summary>
        private static string? Password =>
            Environment.GetEnvironmentVariable(PostgresEnvironment.PASSWORD_NAME);
    }
}