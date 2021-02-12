using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Configuration;
using BoutiqueLoader.Infrastructure.Implementations.Converters;
using BoutiqueLoader.Infrastructure.Interfaces.Converters;

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