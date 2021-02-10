using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Configuration;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Configuration;

namespace BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Configuration
{
    /// <summary>
    /// Конвертер параметров подключения в трансферную модель
    /// </summary>
    public class HostConfigurationTransferConverterMock
    {
        /// <summary>
        /// Конвертер параметров подключения в трансферную модель
        /// </summary>
        public static IHostConfigurationTransferConverter HostConfigurationTransferConverter =>
            new HostConfigurationTransferConverter();
    }
}