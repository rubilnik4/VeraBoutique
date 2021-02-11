using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Configuration;
using BoutiqueDTO.Models.Implementations.Configuration;
using BoutiqueXamarinCommon.Models.Implementation.Configuration;

namespace BoutiqueXamarinCommonXUnit.Data.Transfers.Configuration
{
    /// <summary>
    /// Параметры конфигурации Xamarin. Трансферные модели
    /// </summary>
    public static class XamarinConfigurationTransfersData
    {
        /// <summary>
        /// Параметры конфигурации Xamarin. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<XamarinConfigurationTransfer> XamarinConfigurationTransfers =>
            XamarinConfigurationData.XamarinConfigurationDomains.
            Select(xamarinConfiguration => 
                new XamarinConfigurationTransfer(new HostConfigurationTransfer(xamarinConfiguration.HostConfiguration))).
            ToList();
    }
}