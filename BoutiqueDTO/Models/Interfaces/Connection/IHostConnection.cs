using System;
using System.Net.Http;

namespace BoutiqueDTO.Models.Interfaces.Connection
{
    /// <summary>
    /// Параметры подключения
    /// </summary>
    public interface IHostConnection
    {
        /// <summary>
        /// Имя сервера
        /// </summary>
        Uri Host { get; }

        /// <summary>
        /// Время ожидания
        /// </summary>
        TimeSpan TimeOut { get; }

        /// <summary>
        /// Отключить проверку сертификата
        /// </summary>
        bool DisableSSLValidation { get; }
    }
}