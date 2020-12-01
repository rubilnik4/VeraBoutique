using System;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains
{
    /// <summary>
    /// Одежда. Доменная модель
    /// </summary>
    public class ClothesShortDomain : ClothesMain, IClothesShortDomain
    {
        public ClothesShortDomain(IClothesMain clothesMain, GenderType genderType, string clothesTypeName)
           : this(clothesMain.Id, clothesMain.Name, clothesMain.Description, clothesMain.Price, clothesMain.Image,
                  genderType, clothesTypeName)
        {
            GenderType = genderType;
            ClothesTypeName = clothesTypeName;
        }

        public ClothesShortDomain(int id, string name, string description, decimal price, byte[]? image,
                                  GenderType genderType, string clothesTypeName)
            : base(id, name, description, price, image)
        {
            GenderType = genderType;
            ClothesTypeName = clothesTypeName;
        }

        /// <summary>
        /// Тип пола одежды
        /// </summary>
        public GenderType GenderType { get; }

        /// <summary>
        /// Тип одежды
        /// </summary>
        public string ClothesTypeName { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is IClothesShortDomain clothes && Equals(clothes);

        public bool Equals(IClothesShortDomain? other) =>
            ((IClothesMain?)other)?.Equals(this) == true &&
            GenderType == other.GenderType &&
            ClothesTypeName == other.ClothesTypeName;

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), GenderType, ClothesTypeName);
        #endregion
    }
}