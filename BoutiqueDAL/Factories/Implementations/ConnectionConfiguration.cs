using BoutiqueCommon.Models.Implementation.Connection;
using System;

namespace BoutiqueDAL.Factories.Implementations
{
    /// <summary>
    /// Параметры подключения к базе данных
    /// </summary>
    public class ConnectionConfiguration
    {
        public ConnectionConfiguration(HostConnection hostConnection, string database, Autorisation autorisation)
            : this(hostConnection.Host, hostConnection.Port, database, autorisation.Username, autorisation.Password)
        { }

        public ConnectionConfiguration(string host, int port, string database, string username, string password)
        {
            if (port <= 0) throw new ArgumentOutOfRangeException(nameof(port));

            Host = host;
            Port = port;
            Database = database;
            Username = username;
            Password = password;
        }

        /// <summary>
        /// Сервер
        /// </summary>
        public string Host { get; }

        /// <summary>
        /// Порт
        /// </summary>
        public int Port { get; }

        /// <summary>
        /// Имя базы данных
        /// </summary>
        public string Database { get; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; }

        /// <summary>
        /// Проверка имени базы
        /// </summary>
        public static bool IsDatabaseValid(string? database) => !String.IsNullOrWhiteSpace(database);
    }
}