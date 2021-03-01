using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.CategoryTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.CategoryTransfers;

namespace BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes
{
    /// <summary>
    /// Конвертер типа пола в трансферную модель
    /// </summary>
    public class CategoryTransferConverterMock
    {
        /// <summary>
        /// Конвертер категории одежды в трансферную модель
        /// </summary>
        public static ICategoryTransferConverter CategoryTransferConverter =>
            new CategoryTransferConverter();

        /// <summary>
        /// Конвертер категории одежды с типом в трансферную модель
        /// </summary>
        public static ICategoryClothesTypeTransferConverter CategoryClothesTypeTransferConverter =>
            new CategoryClothesTypeTransferConverter(ClothesTypeTransferConverterMock.ClothesTypeTransferConverter);
    }
}