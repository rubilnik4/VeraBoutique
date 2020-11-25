using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains
{
    /// <summary>
    /// Вид одежды. Базовая доменная модель
    /// </summary>
    public class ClothesTypeShortDomain : ClothesType, IClothesTypeShortDomain
    {
        public ClothesTypeShortDomain(IClothesType clothesType)
            : this(clothesType.Name)
        { }

        public ClothesTypeShortDomain(string name)
          : base(name)
        { }
    }
}