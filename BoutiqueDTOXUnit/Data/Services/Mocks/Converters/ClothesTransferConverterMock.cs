using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTransfers;

namespace BoutiqueDTOXUnit.Data.Services.Mocks.Converters
{
    public class ClothesTransferConverterMock
    {
        /// <summary>
        /// Конвертер информации об одежде в трансферную модель
        /// </summary>
        public static IClothesTransferConverter ClothesTransferConverter =>
            new ClothesTransferConverter(GenderTransferConverterMock.GenderTransferConverter,
                                         ClothesTypeTransferConverterMock.ClothesTypeShortTransferConverter,
                                         ColorClothesTransferConverterMock.ColorTransferConverter,
                                         SizeGroupTransferConverterMock.SizeGroupTransferConverter);

        /// <summary>
        /// Конвертер одежды в трансферную модель
        /// </summary>
        public static IClothesShortTransferConverter ClothesShortTransferConverter =>
            new ClothesShortTransferConverter();
    }
}