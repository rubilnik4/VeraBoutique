using System;
using System.Collections.Generic;
using System.Linq;
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
        public ClothesTypeShortDomain(IClothesType clothesType, string categoryName)
            : this(clothesType.Name, categoryName)
        { }

        public ClothesTypeShortDomain(string name, string categoryName)
          : base(name)
        {
            CategoryName = categoryName;
        }

        /// <summary>
        /// Категория
        /// </summary>
        public string CategoryName { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is IClothesTypeShortDomain clothesTypeDomain && Equals(clothesTypeDomain);

        public bool Equals(IClothesTypeShortDomain? other) =>
            ((IClothesType?)other)?.Equals(this) == true &&
            other?.CategoryName.Equals(CategoryName) == true;

        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), CategoryName);
        #endregion
    }
}