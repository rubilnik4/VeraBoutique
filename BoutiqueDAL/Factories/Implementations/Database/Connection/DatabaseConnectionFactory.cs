﻿using System;
using BoutiqueDAL.Models.Implementations.Connection;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDAL.Factories.Implementations.Database.Connection
{
    /// <summary>
    /// Фабрика создания подключения базы данных
    /// </summary>
    public static class DatabaseConnectionFactory
    {
        /// <summary>
        /// Получить параметры подключения к базе
        /// </summary>
        public static IResultValue<DatabaseConnection> GetDatabaseConnection(string host, string port, string database,
                                                                             string username, string password) =>
            new ResultValue<Func<HostConnection, string, Authorization, DatabaseConnection>>(
                    (hostConnectionIn, databaseIn, authorizationIn) => new DatabaseConnection(hostConnectionIn, databaseIn, authorizationIn)).
            ResultValueCurryOk(GetHostConnection(host, port)).
            ResultValueCurryOk(GetDatabase(database)).
            ResultValueCurryOk(GetAuthorization(username, password)).
            ResultValueOk(getConfiguration => getConfiguration.Invoke());

        /// <summary>
        /// Получить параметры подключения
        /// </summary>
        public static IResultValue<HostConnection> GetHostConnection(string host, string port) =>
            new ResultValue<Func<string, int, HostConnection>>((hostIn, portIn) => new HostConnection(hostIn, portIn)).
            ResultValueCurryOk(GetHost(host)).
            ResultValueCurryOk(GetPort(port)).
            ResultValueOk(getHostConnection => getHostConnection.Invoke());

        /// <summary>
        /// Получить параметры аутентификации
        /// </summary>
        public static IResultValue<Authorization> GetAuthorization(string username, string password) =>
            new ResultValue<Func<string, string, Authorization>>((usernameIn, passwordIn) => new Authorization(usernameIn, passwordIn)).
            ResultValueCurryOk(GetUsername(username)).
            ResultValueCurryOk(GetPassword(password)).
            ResultValueOk(getAuthorization => getAuthorization.Invoke());

        /// <summary>
        /// Получить имя сервера
        /// </summary>
        public static IResultValue<string> GetHost(string host) =>
           host.
           ToResultValueWhereNull(HostConnection.IsHostValid,
                                  _ => ErrorResultFactory.DatabaseConnectionError(nameof(host), "Имя сервера базы данных не задано"));

        /// <summary>
        /// Получить порт
        /// </summary>
        public static IResultValue<int> GetPort(string port) =>
            port.
            ToResultValueWhereNullOkBad(HostConnection.IsPortValid,
                                        Int32.Parse,
                                        _ => ErrorResultFactory.DatabaseConnectionError(nameof(port), "Порт базы данных не задан"));
            
        /// <summary>
        /// Получить имя базы данных
        /// </summary>
        public static IResultValue<string> GetDatabase(string databaseName) =>
            databaseName.
            ToResultValueWhereNull(DatabaseConnection.IsDatabaseNameValid,
                                   _ => ErrorResultFactory.DatabaseConnectionError(nameof(databaseName), "Имя базы данных не задано"));

        /// <summary>
        /// Получить имя пользователя
        /// </summary>
        public static IResultValue<string> GetUsername(string username) =>
            username.
            ToResultValueWhereNull(Authorization.IsUsernameValid,
                                   _ => ErrorResultFactory.DatabaseConnectionError(nameof(username), "Имя пользователя базы данных не задано"));

        /// <summary>
        /// Получить пароль
        /// </summary>
        public static IResultValue<string> GetPassword(string password) =>
            password.
            ToResultValueWhereNull(Authorization.IsPasswordValid,
                                   _ => ErrorResultFactory.DatabaseConnectionError(nameof(password), "Пароль базы данных не задан"));
    }
}