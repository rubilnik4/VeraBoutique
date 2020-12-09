using System;
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
    public class ClothesShortDomain : ClothesShortBase, IClothesShortDomain
    {
        public ClothesShortDomain(IClothesShortBase clothesShort)
           : this(clothesShort.Id, clothesShort.Name, clothesShort.Description, clothesShort.Price, clothesShort.Image,
                  clothesShort.GenderType, clothesShort.ClothesTypeName)
        { }

        public ClothesShortDomain(int id, string name, string description, decimal price, byte[]? image,
                                  GenderType genderType, string clothesTypeName)
            : base(id, name, description, price, image, genderType, clothesTypeName)
        { }
    }
}