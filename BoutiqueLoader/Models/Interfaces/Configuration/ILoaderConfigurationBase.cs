using System;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Common.Interfaces.Configuration;

namespace BoutiqueLoader.Models.Interfaces.Configuration
{
    /// <summary>
    /// Конфигурация консольного загрузчика 
    /// </summary>
    public interface ILoaderConfigurationBase<out THost> : IModel<Guid>
        where THost : IHostConfigurationBase
    {
        /// <summary>
        /// Параметры подключения к серверу
        /// </summary>
        THost HostConfiguration { get; }
    }
}