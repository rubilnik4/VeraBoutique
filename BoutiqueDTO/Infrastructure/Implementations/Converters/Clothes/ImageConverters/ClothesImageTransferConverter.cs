﻿using System;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.Images;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Images;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ImagesConverters;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ImageTransfers;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ImageConverters
{
    /// <summary>
    /// Конвертер изображений в трансферную модель
    /// </summary>
    public class ClothesImageTransferConverter : TransferConverter<Guid, IClothesImageDomain, ClothesImageTransfer>,
                                                 IClothesImageTransferConverter
    {
        /// <summary>
        /// Преобразовать категории одежды в трансферную модель
        /// </summary>
        public override ClothesImageTransfer ToTransfer(IClothesImageDomain clothesImageDomain) =>
            new ClothesImageTransfer(clothesImageDomain);

        /// <summary>
        /// Преобразовать категории одежды из трансферной модели
        /// </summary>
        public override IResultValue<IClothesImageDomain> FromTransfer(ClothesImageTransfer clothesImageTransfer) =>
            new ClothesImageDomain(clothesImageTransfer).
            ToResultValue();
    }
}