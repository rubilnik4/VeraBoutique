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

        #region IEquatable
        public override bool Equals(object? obj) => obj is ILoaderConfigurationBase<THost> loaderConfiguration &&
                                                    Equals(loaderConfiguration);

        public bool Equals(ILoaderConfigurationBase<THost>? other) =>
            other?.HostConfiguration.Equals(HostConfiguration) == true;

        public override int GetHashCode() => HostConfiguration.GetHashCode();
        #endregion
    }
}