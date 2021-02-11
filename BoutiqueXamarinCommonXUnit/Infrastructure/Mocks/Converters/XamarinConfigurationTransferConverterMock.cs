using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Configuration;
using BoutiqueXamarinCommon.Infrastructure.Implementations.Converters;
using BoutiqueXamarinCommon.Infrastructure.Interfaces.Converters;

namespace BoutiqueXamarinCommonXUnit.Infrastructure.Mocks.Converters
{
    /// <summary>
    /// Конвертер конфигурации Xamarin в трансферную модель
    /// </summary>
    public class XamarinConfigurationTransferConverterMock
    {
        /// <summary>
        /// Конвертер конфигурации Xamarin в трансферную модель
        /// </summary>
        public static IXamarinConfigurationTransferConverter XamarinConfigurationTransferConverter =>
            new XamarinConfigurationTransferConverter(HostConfigurationTransferConverterMock.HostConfigurationTransferConverter);
    }
}