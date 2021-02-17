using System;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using BoutiqueDTO.Infrastructure.Implementations.Configuration;
using BoutiqueLoader.Infrastructure.Interfaces.Configuration;
using BoutiqueLoader.Infrastructure.Interfaces.Converters;
using BoutiqueLoader.Models.Implementations.Configuration;
using BoutiqueLoader.Models.Interfaces.Configuration;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Sync;

namespace BoutiqueLoader.Infrastructure.Implementations.Configuration
{
    /// <summary>
    /// Доступ к файлам конфигурации консольного приложения
    /// </summary>
    public class LoaderConfigurationManager : ConsoleConfigurationManager<Guid, ILoaderConfigurationDomain, LoaderConfigurationTransfer>,
                                              ILoaderConfigurationManager
    {
        public LoaderConfigurationManager(ILoaderConfigurationTransferConverter loaderConfigurationTransferConverter)
          : base(loaderConfigurationTransferConverter)
        { }

        /// <summary>
        /// Имя файла конфигурации
        /// </summary>
        protected override string ConfigurationFileName => $"appsettings{ConfigurationAdditional}.json";

        /// <summary>
        /// Параметры для дополнительных конфигураций
        /// </summary>
        private static string ConfigurationAdditional =>
#if DEVELOPMENT
            ".Development";
#elif TEST
            ".Test";
#else
            String.Empty;
#endif
    }
}