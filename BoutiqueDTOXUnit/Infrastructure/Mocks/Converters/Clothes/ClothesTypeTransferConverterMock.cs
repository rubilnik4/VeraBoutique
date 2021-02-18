using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfers;

namespace BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes
{
    /// <summary>
    /// Конвертер полной информации вида одежды в трансферную модель
    /// </summary>
    public class ClothesTypeTransferConverterMock
    {
        /// <summary>
        /// Конвертер полной информации вида одежды в трансферную модель
        /// </summary>
        public static IClothesTypeMainTransferConverter ClothesTypeTransferConverter =>
            new ClothesTypeTransferConverter(CategoryTransferConverterMock.CategoryTransferConverter,
                                             GenderTransferConverterMock.GenderTransferConverter);

        /// <summary>
        /// Конвертер вида одежды в трансферную модель
        /// </summary>
        public static IClothesTypeTransferConverter ClothesTypeShortTransferConverter =>
            new ClothesTypeShortTransferConverter();
    }
}