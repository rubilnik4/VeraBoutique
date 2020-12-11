using System;
using BoutiquePrerequisites.Models.Interfaces.Connection;

namespace BoutiquePrerequisites.Models.Implementations.Connection
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
    }
}