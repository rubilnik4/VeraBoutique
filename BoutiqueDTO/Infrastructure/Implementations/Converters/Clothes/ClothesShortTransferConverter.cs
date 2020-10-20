﻿using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes
{
    /// <summary>
    /// Конвертер одежды в трансферную модель
    /// </summary>
    public class ClothesShortTransferConverter : TransferConverter<int, IClothesShortDomain, ClothesShortTransfer>,
                                                 IClothesShortTransferConverter
    {
        /// <summary>
        /// Преобразовать категории одежды в трансферную модель
        /// </summary>
        public override ClothesShortTransfer ToTransfer(IClothesShortDomain clothesShortDomain) =>
            new ClothesShortTransfer(clothesShortDomain.Id, clothesShortDomain.Name, 
                                     clothesShortDomain.Price, clothesShortDomain.Image);

        /// <summary>
        /// Преобразовать категории одежды из трансферной модели
        /// </summary>
        public override IClothesShortDomain FromTransfer(ClothesShortTransfer clothesShortTransfer) =>
            new ClothesShortDomain(clothesShortTransfer.Id, clothesShortTransfer.Name,
                                   clothesShortTransfer.Price, clothesShortTransfer.Image);
    }
}