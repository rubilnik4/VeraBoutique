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
        public HostConnection(Uri host, TimeSpan timeOut)
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
        public TimeSpan TimeOut { get; }
    }
}