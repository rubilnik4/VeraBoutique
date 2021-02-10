using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;

namespace BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes
{
    /// <summary>
    /// Конвертер типа пола в трансферную модель
    /// </summary>
    public class GenderTransferConverterMock
    {
        /// <summary>
        /// Конвертер типа пола в трансферную модель
        /// </summary>
        public static IGenderTransferConverter GenderTransferConverter =>
            new GenderTransferConverter();
    }
}