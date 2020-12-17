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
        int TimeOut { get; }

        /// <summary>
        /// Преобразовать в http клиент
        /// </summary>
        HttpClient ToHttpClient();
    }
}