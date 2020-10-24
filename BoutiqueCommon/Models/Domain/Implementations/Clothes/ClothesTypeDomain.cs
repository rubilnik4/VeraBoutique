using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes
{
    /// <summary>
    /// Вид одежды. Доменная модель
    /// </summary>
    public class ClothesTypeDomain: ClothesType, IClothesTypeDomain, IEquatable<IClothesTypeDomain>
    {
        public ClothesTypeDomain(string name, ICategoryDomain categoryDomain)
          : base(name)
        {
            CategoryDomain = categoryDomain;
        }

        /// <summary>
        /// Категория одежды. Доменная модель
        /// </summary>
        public ICategoryDomain CategoryDomain { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is IClothesTypeDomain clothesTypeDomain && Equals(clothesTypeDomain);

        public bool Equals(IClothesTypeDomain? other) =>
            other?.Id == Id && 
            other?.CategoryDomain.Equals(CategoryDomain) == true;

        public override int GetHashCode() => HashCode.Combine(Name, CategoryDomain.GetHashCode());
        #endregion
    }
}