using System;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains
{
    /// <summary>
    /// Вид одежды. Доменная модель
    /// </summary>
    public  abstract class ClothesTypeDomain : ClothesType, IClothesTypeDomain, IEquatable<IClothesTypeDomain>
    {
        protected ClothesTypeDomain(string name, ICategoryDomain category)
         : base(name)
        {
            Category = category;
        }

        /// <summary>
        /// Категория одежды. Доменная модель
        /// </summary>
        public ICategoryDomain Category { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is IClothesTypeDomain clothesTypeDomain && Equals(clothesTypeDomain);

        public bool Equals(IClothesTypeDomain? other) =>
            other?.Id == Id &&
            other?.Category.Equals(Category) == true;

        public override int GetHashCode() => HashCode.Combine(Name, Category.GetHashCode());
        #endregion
    }
}