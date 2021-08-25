using System;

namespace BoutiqueDAL.Models.Implementations.Connection
{
    /// <summary>
    /// Параметры подключения
    /// </summary>
    public class HostConnection : IEquatable<HostConnection>
    {
        public HostConnection(string host, int port)
        {
            if (!IsHostValid(host)) throw new ArgumentNullException(nameof(host));
            if (!IsPortInRange(port)) throw new ArgumentOutOfRangeException(nameof(port));

            Host = host;
            Port = port;
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
        /// Проверка имени сервера
        /// </summary>
        public static bool IsHostValid(string? host) => 
            !String.IsNullOrWhiteSpace(host);

        /// <summary>
        /// Проверка порта
        /// </summary>
        public static bool IsPortValid(string? port) =>
            Int32.TryParse(port, out int portInt) &&
            IsPortInRange(portInt);

        /// <summary>
        /// Проверка порта по диапазону
        /// </summary>
        public static bool IsPortInRange(int port) =>
            port > 0;

        #region IEquatable
        public override bool Equals(object? obj) => obj is HostConnection hostConnection && Equals(hostConnection);

        public bool Equals(HostConnection? other) =>
            other?.Host == Host &&
            other?.Port == Port;

        public override int GetHashCode() => HashCode.Combine(Host, Port);
        #endregion
    }
}