using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Configuration;
using BoutiqueConsole.Models.Implementations.Configuration;
using BoutiqueConsole.Models.Interfaces.Configuration;

namespace BoutiqueLoaderXUnit.Data.Transfers.Configuration
{
    /// <summary>
    /// Параметры конфигурации Xamarin. Доменные модели
    /// </summary>
    public static class LoaderConfigurationData
    {
        /// <summary>
        /// Параметры конфигурации Xamarin. Доменные модели
        /// </summary>
        public static IReadOnlyCollection<ILoaderConfigurationDomain> LoaderConfigurationDomains =>
            new List<LoaderConfigurationDomain>
            {
                new (HostConfigurationData.HostConfigurationDomains.First()),
                new (HostConfigurationData.HostConfigurationDomains.Last()),
            };
    }
}