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
            :this(host, timeOut, false)
        { }

        public HostConnection(Uri host, TimeSpan timeOut, bool disableSslValidation)
        {
            Host = host;
            TimeOut = timeOut;
            DisableSSLValidation = disableSslValidation;
        }
        
        /// <summary>
        /// Имя сервера
        /// </summary>
       public Uri Host { get; }

        /// <summary>
        /// Время ожидания
        /// </summary>
        public TimeSpan TimeOut { get; }

        /// <summary>
        /// Отключить проверку сертификата
        /// </summary>
        public bool DisableSSLValidation { get; }
    }
}