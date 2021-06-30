using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTransfers;

namespace BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes
{
    public class ClothesTransferConverterMock
    {
        /// <summary>
        /// Конвертер информации об одежде в трансферную модель
        /// </summary>
        public static IClothesMainTransferConverter ClothesMainTransferConverter =>
            new ClothesMainTransferConverter(GenderTransferConverterMock.GenderTransferConverter,
                                             ClothesTypeTransferConverterMock.ClothesTypeTransferConverter,
                                             ColorClothesTransferConverterMock.ColorTransferConverter,
                                             SizeGroupTransferConverterMock.SizeGroupMainTransferConverter);

        /// <summary>
        /// Конвертер уточненной информации об одежде в трансферную модель
        /// </summary>
        public static IClothesDetailTransferConverter ClothesDetailTransferConverter =>
            new ClothesDetailTransferConverter(ColorClothesTransferConverterMock.ColorTransferConverter,
                                               SizeGroupTransferConverterMock.SizeGroupMainTransferConverter);

        /// <summary>
        /// Конвертер одежды в трансферную модель
        /// </summary>
        public static IClothesTransferConverter ClothesTransferConverter =>
            new ClothesTransferConverter();
    }
}