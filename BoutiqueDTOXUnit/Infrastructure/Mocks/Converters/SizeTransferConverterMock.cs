using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;

namespace BoutiqueDTOXUnit.Infrastructure.Mocks.Converters
{
    /// <summary>
    /// Конвертер размеров одежды в трансферную модель
    /// </summary>
    public class SizeTransferConverterMock
    {
        /// <summary>
        /// Конвертер размеров одежды в трансферную модель
        /// </summary>
        public static ISizeTransferConverter SizeTransferConverter =>
            new SizeTransferConverter();
    }
}