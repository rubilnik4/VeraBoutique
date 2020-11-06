﻿using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Clothes.ClothesTypeTransfers
{
    /// <summary>
    /// Вид одежды. Базовая трансферная модель
    /// </summary>
    public interface IClothesTypeShortTransfer : IClothesType, ITransferModel<string>
    {
        /// <summary>
        /// Категория одежды. Трансферная модель
        /// </summary>
        CategoryTransfer Category { get; }
    }
}