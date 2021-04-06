using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Implementations.Clothes.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains
{
    /// <summary>
    /// Одежда. Доменная модель
    /// </summary>
    public class ClothesDomain : ClothesBase, IClothesDomain
    {
        public ClothesDomain(IClothesBase clothes)
           : this(clothes.Id, clothes.Name, clothes.Description, clothes.Price, clothes.GenderType, clothes.ClothesTypeName)
        { }

        public ClothesDomain(int id, string name, string description, decimal price,
                             GenderType genderType, string clothesTypeName)
            : base(id, name, description, price, genderType, clothesTypeName)
        { }
    }
}