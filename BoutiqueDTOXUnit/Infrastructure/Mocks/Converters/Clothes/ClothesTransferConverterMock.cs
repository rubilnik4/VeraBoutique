﻿using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTransfers;
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
                                         ClothesTypeTransferConverterMock.ClothesTypeShortTransferConverter,
                                         ColorClothesTransferConverterMock.ColorTransferConverter,
                                         SizeGroupTransferConverterMock.SizeGroupTransferConverter);

        /// <summary>
        /// Конвертер одежды в трансферную модель
        /// </summary>
        public static IClothesTransferConverter ClothesTransferConverter =>
            new ClothesTransferConverter();
    }
}