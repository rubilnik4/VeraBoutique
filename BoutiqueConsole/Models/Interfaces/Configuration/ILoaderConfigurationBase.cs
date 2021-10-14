using System;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Common.Interfaces.Configuration;

namespace BoutiqueConsole.Models.Interfaces.Configuration
{
    /// <summary>
    /// Конфигурация консольного загрузчика 
    /// </summary>
    public interface ILoaderConfigurationBase<THost> : IModel<Guid>, IEquatable<ILoaderConfigurationBase<THost>>
        where THost : IHostConfigurationBase
    {
        /// <summary>
        /// Параметры подключения к серверу
        /// </summary>
        THost HostConfiguration { get; }
    }
}