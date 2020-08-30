using System;
using BoutiqueDAL.Models.Implementations.Connection;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Factories.Implementations.Database.Base
{
    /// <summary>
    /// Фабрика создания подключения базы данных
    /// </summary>
    public static class DatabaseConnectionFactory
    {
        /// <summary>
        /// Получить параметры подключения к базе
        /// </summary>
        public static IResultValue<DatabaseConnection> GetDatabaseConnection(IResultValue<HostConnection> hostConnection,
                                                                             IResultValue<string> database,
                                                                             IResultValue<Authorization> authorization) =>
            new ResultValue<Func<HostConnection, string, Authorization, DatabaseConnection>>(
                    (hostConnectionIn, databaseIn, authorizationIn) => new DatabaseConnection(hostConnectionIn, databaseIn, authorizationIn)).
            ResultCurryOkBind(hostConnection).
            ResultCurryOkBind(database).
            ResultCurryOkBind(authorization).
            ResultValueOk(getConfiguration => getConfiguration.Invoke());

        /// <summary>
        /// Получить параметры подключения
        /// </summary>
        public static IResultValue<HostConnection> GetHostConnection(string? host, string? port) =>
            new ResultValue<Func<string, int, HostConnection>>((hostIn, portIn) => new HostConnection(hostIn, portIn)).
            ResultCurryOkBind(GetHost(host)).
            ResultCurryOkBind(GetPort(port)).
            ResultValueOk(getHostConnection => getHostConnection.Invoke());

        /// <summary>
        /// Получить параметры аутентификации
        /// </summary>
        public static IResultValue<Authorization> GetAuthorization(string? username, string? password) =>
            new ResultValue<Func<string, string, Authorization>>((usernameIn, passwordIn) => new Authorization(usernameIn, passwordIn)).
            ResultCurryOkBind(GetUsername(username)).
            ResultCurryOkBind(GetPassword(password)).
            ResultValueOk(getAuthorization => getAuthorization.Invoke());

        /// <summary>
        /// Получить имя сервера
        /// </summary>
        public static IResultValue<string> GetHost(string? host) =>
           host.
           WhereContinue(HostConnection.IsHostValid,
               okFunc: _ => new ResultValue<string>(host!),
               badFunc: _ => new ErrorResult(ErrorResultType.DatabaseIncorrectConnection, "Имя сервера базы данных не задано").
                                 ToResultValue<string>());

        /// <summary>
        /// Получить порт
        /// </summary>
        public static IResultValue<int> GetPort(string? port) =>
            port.
            WhereContinue(HostConnection.IsPortValid,
                okFunc: _ => new ResultValue<int>(Int32.Parse(port!)),
                badFunc: _ => new ErrorResult(ErrorResultType.DatabaseIncorrectConnection, "Порт базы данных не задан").
                                  ToResultValue<int>());

        /// <summary>
        /// Получить имя базы данных
        /// </summary>
        public static IResultValue<string> GetDatabase(string? database) =>
            database.
            WhereContinue(DatabaseConnection.IsDatabaseValid,
                okFunc: _ => new ResultValue<string>(database!),
                badFunc: _ => new ErrorResult(ErrorResultType.DatabaseIncorrectConnection, "Имя базы данных не задано").
                              ToResultValue<string>());

        /// <summary>
        /// Получить имя пользователя
        /// </summary>
        public static IResultValue<string> GetUsername(string? username) =>
            username.
            WhereContinue(Authorization.IsUsernameValid,
                okFunc: _ => new ResultValue<string>(username!),
                badFunc: _ => new ErrorResult(ErrorResultType.DatabaseIncorrectConnection, "Имя пользователя базы данных не задано").
                              ToResultValue<string>());

        /// <summary>
        /// Получить пароль
        /// </summary>
        public static IResultValue<string> GetPassword(string? password) =>
            password.
            WhereContinue(Authorization.IsPasswordValid,
                okFunc: _ => new ResultValue<string>(password!),
                badFunc: _ => new ErrorResult(ErrorResultType.DatabaseIncorrectConnection, "Пароль базы данных не задан").
                              ToResultValue<string>());
    }
}