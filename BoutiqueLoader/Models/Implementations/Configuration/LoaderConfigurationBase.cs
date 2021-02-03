using System;
using BoutiqueCommon.Models.Common.Interfaces.Configuration;
using BoutiqueLoader.Models.Interfaces.Configuration;

namespace BoutiqueLoader.Models.Implementations.Configuration
{
    /// <summary>
    /// Конфигурация консольного загрузчика 
    /// </summary>
    public class LoaderConfigurationBase<THost>: ILoaderConfigurationBase<THost>
         where THost : IHostConfigurationBase
    {
        public LoaderConfigurationBase(THost hostConfiguration)
        {
            HostConfiguration = hostConfiguration;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; } = Guid.NewGuid();

        /// <summary>
        /// Параметры подключения к серверу
        /// </summary>
        public THost HostConfiguration { get; }
    }
}