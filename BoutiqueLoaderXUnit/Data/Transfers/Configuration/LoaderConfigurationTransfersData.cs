using System.Collections.Generic;
using System.Linq;
using BoutiqueDTO.Models.Implementations.Configuration;
using BoutiqueLoader.Models.Implementations.Configuration;

namespace BoutiqueLoaderXUnit.Data.Transfers.Configuration
{
    /// <summary>
    /// Параметры конфигурации Xamarin. Трансферные модели
    /// </summary>
    public static class LoaderConfigurationTransfersData
    {
        /// <summary>
        /// Параметры конфигурации Xamarin. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<LoaderConfigurationTransfer> LoaderConfigurationTransfers =>
            LoaderConfigurationData.LoaderConfigurationDomains.
            Select(loaderConfiguration => 
                new LoaderConfigurationTransfer(new HostConfigurationTransfer(loaderConfiguration.HostConfiguration))).
            ToList();
    }
}