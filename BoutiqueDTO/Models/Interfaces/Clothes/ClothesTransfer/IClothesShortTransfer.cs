﻿using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Clothes.ClothesTransfer
{
    /// <summary>
    /// Одежда. Трансферная модель
    /// </summary>
    public interface IClothesShortTransfer : IClothesShort, ITransferModel<int>
    { }
}