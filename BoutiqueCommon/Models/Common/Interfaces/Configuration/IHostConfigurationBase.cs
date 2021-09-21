using System;
using BoutiqueCommon.Models.Common.Interfaces.Base;

namespace BoutiqueCommon.Models.Common.Interfaces.Configuration
{
    /// <summary>
    /// Параметры подключения к серверу
    /// </summary>
    public interface IHostConfigurationBase: IModel<string>
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
        bool DisableSSL { get; }
    }
}