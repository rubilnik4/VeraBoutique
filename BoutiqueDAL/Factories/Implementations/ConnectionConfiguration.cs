using System;

namespace BoutiqueDAL.Factories.Implementations
{
    /// <summary>
    /// Параметры подключения к базе данных
    /// </summary>
    public class ConnectionConfiguration
    {
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
    }
}