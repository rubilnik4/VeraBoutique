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
        public static IClothesTypeMainTransferConverter ClothesTypeMainTransferConverter =>
            new ClothesTypeMainTransferConverter(CategoryTransferConverterMock.CategoryTransferConverter);

        /// <summary>
        /// Конвертер вида одежды в трансферную модель
        /// </summary>
        public static IClothesTypeTransferConverter ClothesTypeTransferConverter =>
            new ClothesTypeTransferConverter();
    }
}