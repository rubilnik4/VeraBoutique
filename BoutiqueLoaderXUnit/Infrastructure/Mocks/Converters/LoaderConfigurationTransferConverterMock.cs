using BoutiqueConsole.Infrastructure.Implementations.Converters;
using BoutiqueConsole.Infrastructure.Interfaces.Converters;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Configuration;

namespace BoutiqueLoaderXUnit.Infrastructure.Mocks.Converters
{
    /// <summary>
    /// Конвертер конфигурации консольного загрузчика в трансферную модель
    /// </summary>
    public class LoaderConfigurationTransferConverterMock
    {
        /// <summary>
        /// Конвертер конфигурации консольного загрузчика в трансферную модель
        /// </summary>
        public static ILoaderConfigurationTransferConverter LoaderConfigurationTransferConverter =>
            new LoaderConfigurationTransferConverter(HostConfigurationTransferConverterMock.HostConfigurationTransferConverter);
    }
}