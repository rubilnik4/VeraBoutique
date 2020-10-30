using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesType;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes
{
    /// <summary>
    /// Вид одежды. Основные данные. Доменная модель
    /// </summary>
    public class ClothesTypeShortDomain: ClothesType, IClothesTypeShortDomain
    {
        public ClothesTypeShortDomain(string name)
          : base(name)
        { }
    }
}