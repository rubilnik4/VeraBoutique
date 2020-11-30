using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfers;

namespace BoutiqueDTOXUnit.Data.Services.Mocks.Converters
{
    /// <summary>
    /// Конвертер полной информации вида одежды в трансферную модель
    /// </summary>
    public class ClothesTypeTransferConverterMock
    {
        /// <summary>
        /// Конвертер полной информации вида одежды в трансферную модель
        /// </summary>
        public static IClothesTypeTransferConverter ClothesTypeTransferConverter =>
            new ClothesTypeTransferConverter(CategoryTransferConverterMock.CategoryTransferConverter,
                                             GenderTransferConverterMock.GenderTransferConverter);

        /// <summary>
        /// Конвертер вида одежды в трансферную модель
        /// </summary>
        public static IClothesTypeShortTransferConverter ClothesTypeShortTransferConverter =>
            new ClothesTypeShortTransferConverter();
    }
}