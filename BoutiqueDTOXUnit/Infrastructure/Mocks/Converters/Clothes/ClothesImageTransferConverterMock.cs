using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ImageConverters;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ImagesConverters;

namespace BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes
{
    /// <summary>
    /// Конвертер цвета одежды в трансферную модель
    /// </summary>
    public static class ClothesImageTransferConverterMock
    {
        /// <summary>
        /// Конвертер цвета одежды в трансферную модель
        /// </summary>
        public static IClothesImageTransferConverter ClothesImageTransferConverter =>
            new ClothesImageTransferConverter();
    }
}