using System;
using BoutiqueCommon.Models.Common.Interfaces.Configuration;

namespace BoutiqueCommon.Models.Common.Implementations.Configuration
{
    /// <summary>
    /// Параметры подключения к серверу
    /// </summary>
    public abstract class HostConfigurationBase: IHostConfigurationBase
    {
        protected HostConfigurationBase(Uri host, TimeSpan timeOut, bool disableSsl)
        {
            Host = host;
            TimeOut = timeOut;
            DisableSSL = disableSsl;
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

        /// <summary>
        /// Отключить проверку сертификата
        /// </summary>
        public bool DisableSSL { get; }
    }
}