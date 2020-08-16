using System;
using BoutiqueCommon.Models.Implementation.Connection;
using BoutiqueDAL.Factories.Implementations;
using Functional.FunctionalExtensions;
using Functional.FunctionalExtensions.ResultExtension;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueMVC.DependencyInjection
{
    /// <summary>
    /// Параметры подключения базы Postgres
    /// </summary>
    public static class PostgresConfiguration
    {
        /// <summary>
        /// Получить параметры подключения к базе из переменных окружения
        /// </summary>
        public static IResultValue<ConnectionConfiguration> GetPostgresConfiguration() =>
            new ResultValue<Func<HostConnection, string, Autorisation, ConnectionConfiguration>>(
                    (hostConnection, database, autorisation) => new ConnectionConfiguration(hostConnection, database, autorisation)).
                ResultCurryOkBind(GetHostConnection()).
                ResultCurryOkBind(GetDatabase()).
                ResultCurryOkBind(GetAutorisation()).
                ResultValueOk(getConfiguration => getConfiguration.Invoke());

        /// <summary>
        /// Получить параметры подключения
        /// </summary>
        private static IResultValue<HostConnection> GetHostConnection() =>
            new ResultValue<Func<string, int, HostConnection>>((host, port) => new HostConnection(host, port)).
                ResultCurryOkBind(GetHost()).
                ResultCurryOkBind(GetPort()).
                ResultValueOk(getHostConnection => getHostConnection.Invoke());

        /// <summary>
        /// Получить параметры аутентификации
        /// </summary>
        private static IResultValue<Autorisation> GetAutorisation() =>
            new ResultValue<Func<string, string, Autorisation>>((username, password) => new Autorisation(username, password)).
                ResultCurryOkBind(GetUsername()).
                ResultCurryOkBind(GetPassword()).
                ResultValueOk(getAutorisation => getAutorisation.Invoke());

        /// <summary>
        /// Получить имя сервера
        /// </summary>
        private static IResultValue<string> GetHost() =>
            Environment.GetEnvironmentVariable(PostgresEnvironment.HOST_NAME).
            WhereContinue(HostConnection.IsHostValid,
                okFunc: host => new ResultValue<string>(host!),
                badFunc: _ => new ErrorResult(ErrorResultType.IncorrectDatabaseConnection, "Имя сервера базы данных не задано").
                                  ToResultValue<string>());

        /// <summary>
        /// Получить порт
        /// </summary>
        private static IResultValue<int> GetPort() =>
            Environment.GetEnvironmentVariable(PostgresEnvironment.PORT_NAME).
            WhereContinue(HostConnection.IsPortValid,
                okFunc: port => new ResultValue<int>(Int32.Parse(port!)),
                badFunc: _ => new ErrorResult(ErrorResultType.IncorrectDatabaseConnection, "Порт базы данных не задан").
                                  ToResultValue<int>());

        /// <summary>
        /// Получить имя базы данных
        /// </summary>
        private static IResultValue<string> GetDatabase() =>
            Environment.GetEnvironmentVariable(PostgresEnvironment.DATABASE_NAME).
            WhereContinue(ConnectionConfiguration.IsDatabaseValid,
                okFunc: database => new ResultValue<string>(database!),
                badFunc: _ => new ErrorResult(ErrorResultType.IncorrectDatabaseConnection, "Имя базы данных не задано").
                              ToResultValue<string>());

        /// <summary>
        /// Получить имя пользователя
        /// </summary>
        private static IResultValue<string> GetUsername() =>
            Environment.GetEnvironmentVariable(PostgresEnvironment.USER_NAME).
            WhereContinue(Autorisation.IsUsernameValid,
                okFunc: username => new ResultValue<string>(username!),
                badFunc: _ => new ErrorResult(ErrorResultType.IncorrectDatabaseConnection, "Имя пользователя базы данных не задано").
                              ToResultValue<string>());

        /// <summary>
        /// Получить пароль
        /// </summary>
        private static IResultValue<string> GetPassword() =>
            Environment.GetEnvironmentVariable(PostgresEnvironment.PASSWORD_NAME).
            WhereContinue(Autorisation.IsPasswordValid,
                okFunc: username => new ResultValue<string>(username!),
                badFunc: _ => new ErrorResult(ErrorResultType.IncorrectDatabaseConnection, "Пароль базы данных не задан").
                              ToResultValue<string>());
    }
}