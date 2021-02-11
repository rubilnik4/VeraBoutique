using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Configuration;
using BoutiqueDTO.Models.Implementations.Configuration;
using BoutiqueXamarinCommon.Models.Implementation.Configuration;
using BoutiqueXamarinCommon.Models.Interfaces.Configuration;

namespace BoutiqueXamarinCommonXUnit.Data.Transfers.Configuration
{
    /// <summary>
    /// Параметры конфигурации Xamarin. Доменные модели
    /// </summary>
    public static class XamarinConfigurationData
    {
        /// <summary>
        /// Параметры конфигурации Xamarin. Доменные модели
        /// </summary>
        public static IReadOnlyCollection<IXamarinConfigurationDomain> XamarinConfigurationDomains =>
            new List<XamarinConfigurationDomain>()
            {
                new (HostConfigurationData.HostConfigurationDomains.First()),
                new (HostConfigurationData.HostConfigurationDomains.Last()),
            };
    }
}