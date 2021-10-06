using System;
using System.Collections.Generic;
using System.Configuration;
using BoutiqueDAL.Factories.Implementations.Database.Connection;
using BoutiqueDAL.Models.Implementations.Connection;
using BoutiqueDAL.Models.Implementations.Identities;
using BoutiqueMVC.Models.Implementations.Environment;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
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
        public static DatabaseConnection PostgresConnection =>
            DatabaseConnectionFactory.GetDatabaseConnection(Host, Port, Database, Username, Password).
            WhereContinue(result => result.OkStatus,
                          result => result.Value,
                          _ => throw new ArgumentNullException(nameof(PostgresConnection)));
    
        /// <summary>
        /// Имя сервера
        /// </summary>
        private static string Host =>
            Environment.GetEnvironmentVariable(PostgresEnvironment.HOST_NAME) ??
            throw new ConfigurationErrorsException();

        /// <summary>
        /// Порт
        /// </summary>
        private static string Port =>
            Environment.GetEnvironmentVariable(PostgresEnvironment.PORT_NAME) ??
            throw new ConfigurationErrorsException();

        /// <summary>
        /// Имя базы данных
        /// </summary>
        private static string Database =>
            Environment.GetEnvironmentVariable(PostgresEnvironment.DATABASE_NAME) ??
            throw new ConfigurationErrorsException();

        /// <summary>
        /// Имя пользователя
        /// </summary>
        private static string Username =>
            Environment.GetEnvironmentVariable(PostgresEnvironment.USER_NAME) ??
            throw new ConfigurationErrorsException();

        /// <summary>
        /// Пароль
        /// </summary>
        private static string Password =>
            Environment.GetEnvironmentVariable(PostgresEnvironment.PASSWORD_NAME) ??
            throw new ConfigurationErrorsException();
    }
}