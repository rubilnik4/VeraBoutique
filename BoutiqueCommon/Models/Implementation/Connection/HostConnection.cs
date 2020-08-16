using System;

namespace BoutiqueCommon.Models.Implementation.Connection
{
    /// <summary>
    /// Параметры подключения
    /// </summary>
    public class HostConnection
    {
        public HostConnection(string host, int port)
        {
            if (String.IsNullOrWhiteSpace(Host)) throw new ArgumentNullException(nameof(Host));
            if (port < 0) throw new ArgumentOutOfRangeException(nameof(port));

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
        public static bool IsHostValid(string? host) => !String.IsNullOrWhiteSpace(host);

        /// <summary>
        /// Проверка порта
        /// </summary>
        public static bool IsPortValid(string? port) => Int32.TryParse(port, out _);
    }
}