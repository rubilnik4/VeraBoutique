using System;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains
{
    /// <summary>
    /// Одежда. Базовая доменная модель
    /// </summary>
    public interface IClothesShortDomain : IClothesShortBase, IDomainModel<int>
    { }
}