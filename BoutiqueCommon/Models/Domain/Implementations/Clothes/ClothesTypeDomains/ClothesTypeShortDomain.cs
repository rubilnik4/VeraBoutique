using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Implementations.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains
{
    /// <summary>
    /// Вид одежды. Базовая доменная модель
    /// </summary>
    public class ClothesTypeShortDomain : ClothesTypeShortBase, IClothesTypeShortDomain
    {
        public ClothesTypeShortDomain(IClothesTypeShortBase clothesType)
            : this(clothesType.Name, clothesType.CategoryName)
        { }

        public ClothesTypeShortDomain(string name, string categoryName)
          : base(name, categoryName)
        { }
    }
}