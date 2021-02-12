using System;
using BoutiqueCommon.Models.Common.Interfaces.Configuration;
using BoutiqueXamarinCommon.Models.Interfaces.Configuration;

namespace BoutiqueXamarinCommon.Models.Implementation.Configuration
{
    /// <summary>
    /// Конфигурация
    /// </summary>
    public class XamarinConfigurationBase<THost>: IXamarinConfigurationBase<THost>
         where THost : IHostConfigurationBase
    {
        public XamarinConfigurationBase(THost hostConfiguration)
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
        public override bool Equals(object? obj) => obj is IXamarinConfigurationBase<THost> xamarinConfiguration && 
                                                    Equals(xamarinConfiguration);

        public bool Equals(IXamarinConfigurationBase<THost>? other) =>
            other?.HostConfiguration.Equals(HostConfiguration) == true;

        public override int GetHashCode() => HostConfiguration.GetHashCode();
        #endregion
    }
}