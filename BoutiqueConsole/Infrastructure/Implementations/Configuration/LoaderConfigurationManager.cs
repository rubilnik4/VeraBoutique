using System;
using BoutiqueConsole.Infrastructure.Interfaces.Configuration;
using BoutiqueConsole.Infrastructure.Interfaces.Converters;
using BoutiqueConsole.Models.Implementations.Configuration;
using BoutiqueConsole.Models.Interfaces.Configuration;

namespace BoutiqueConsole.Infrastructure.Implementations.Configuration
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
#if DEBUG
            ".Development";
#elif TEST
            ".Test";
#else
            String.Empty;
#endif
    }
}