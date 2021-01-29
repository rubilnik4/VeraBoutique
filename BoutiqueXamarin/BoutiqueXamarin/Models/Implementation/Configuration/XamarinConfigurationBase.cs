using System;
using BoutiqueCommon.Models.Common.Interfaces.Configuration;
using BoutiqueXamarin.Models.Interfaces.Configuration;

namespace BoutiqueXamarin.Models.Implementation.Configuration
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
    }
}