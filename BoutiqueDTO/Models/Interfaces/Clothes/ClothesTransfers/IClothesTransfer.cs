﻿using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Clothes.ClothesTransfers
{
    /// <summary>
    /// Одежда. Базовая трансферная модель
    /// </summary>
    public interface IClothesTransfer : IClothesBase, ITransferModel<int>
    { }
}