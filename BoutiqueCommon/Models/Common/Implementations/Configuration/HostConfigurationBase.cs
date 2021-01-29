using System;
using BoutiqueCommon.Models.Common.Interfaces.Configuration;

namespace BoutiqueCommon.Models.Common.Implementations.Configuration
{
    /// <summary>
    /// Параметры подключения к серверу
    /// </summary>
    public abstract class HostConfigurationBase: IHostConfigurationBase
    {
        protected HostConfigurationBase(Uri host, TimeSpan timeOut)
        {
            Host = host;
            TimeOut = timeOut;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id => Host.Host;

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