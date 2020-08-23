using System;

namespace BoutiqueDAL.Models.Implementations.Connection
{
    /// <summary>
    /// Параметры подключения к базе данных
    /// </summary>
    public class DatabaseConnection : IEquatable<DatabaseConnection>
    {
        public DatabaseConnection(HostConnection hostConnection, string database, Authorization authorization)
        {
            if (!IsDatabaseValid(database)) throw new ArgumentNullException(nameof(database));

            HostConnection = hostConnection;
            Database = database;
            Authorization = authorization;
        }

        /// <summary>
        /// Параметры подключения
        /// </summary>
        public HostConnection HostConnection { get; }

        /// <summary>
        /// Имя базы данных
        /// </summary>
        public string Database { get; }

        /// <summary>
        /// Параметры авторизации
        /// </summary>
        public Authorization Authorization { get; }

        /// <summary>
        /// Сервер
        /// </summary>
        public string Host => HostConnection.Host;

        /// <summary>
        /// Порт
        /// </summary>
        public int Port => HostConnection.Port;

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Username => Authorization.Username;

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password => Authorization.Password;

        /// <summary>
        /// Строка подключения к базе
        /// </summary>
        public string ConnectionString => $"Host={Host};Port={Port};Database={Database};Username={Username};Password={Password}";

        /// <summary>
        /// Проверка имени базы
        /// </summary>
        public static bool IsDatabaseValid(string? database) => !String.IsNullOrWhiteSpace(database);

        #region IEquatable
        public override bool Equals(object? obj) =>
            obj is DatabaseConnection databaseConnection && Equals(databaseConnection);

        public bool Equals(DatabaseConnection? other) =>
            other?.HostConnection.Equals(HostConnection) == true &&
            other?.Database == Database &&
            other?.Authorization.Equals(Authorization) == true;

        public override int GetHashCode() => HashCode.Combine(HostConnection, Database, Authorization);
        #endregion
    }
}