using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;

namespace BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes
{
    /// <summary>
    /// Конвертер цвета одежды в трансферную модель
    /// </summary>
    public static class ColorTransferConverterMock
    {
        /// <summary>
        /// Конвертер цвета одежды в трансферную модель
        /// </summary>
        public static IColorTransferConverter ColorTransferConverter =>
            new ColorTransferConverter();
    }
}