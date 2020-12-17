using System;
using System.Net.Http;
using BoutiqueDTO.Models.Interfaces.Connection;

namespace BoutiqueDTO.Models.Implementations.Connection
{
    /// <summary>
    /// Параметры подключения
    /// </summary>
    public class HostConnection: IHostConnection
    {
        public HostConnection(Uri host, int timeOut)
        {
            Host = host;
            TimeOut = timeOut;
        }
        
        /// <summary>
        /// Имя сервера
        /// </summary>
       public Uri Host { get; }

        /// <summary>
        /// Время ожидания
        /// </summary>
        public int TimeOut { get; }

        /// <summary>
        /// Преобразовать в http клиент
        /// </summary>
        public HttpClient ToHttpClient() =>
            new ()
            {
                BaseAddress = Host,
                Timeout = TimeSpan.FromSeconds(TimeOut),
            };
    }
}