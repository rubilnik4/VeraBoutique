﻿using BoutiqueCommon.Models.Common.Implementations.Clothes.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains
{
    /// <summary>
    /// Одежда. Доменная модель
    /// </summary>
    public class ClothesShortDomain : ClothesShort, IClothesShortDomain
    {
        public ClothesShortDomain(int id, string name, decimal price, byte[]? image)
            : base(id, name, price, image)
        { }
    }
}