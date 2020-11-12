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
        public ClothesTypeShortDomain(IClothesType clothesType, ICategoryDomain category)
            : this(clothesType.Name, category)
        { }

        public ClothesTypeShortDomain(string name, ICategoryDomain category)
          : base(name)
        {
            Category = category;
        }

        /// <summary>
        /// Категория одежды. Доменная модель
        /// </summary>
        public ICategoryDomain Category { get; }

        /// <summary>
        /// Преобразовать в полную версию
        /// </summary>
        public IClothesTypeDomain ToClothesTypeDomain(IGenderDomain genders) =>
            ToClothesTypeDomain(new List<IGenderDomain> { genders });

        /// <summary>
        /// Преобразовать в полную версию
        /// </summary>
        public IClothesTypeDomain ToClothesTypeDomain(IEnumerable<IGenderDomain> genders) =>
            new ClothesTypeDomain(this, genders);

        #region IEquatable
        public override bool Equals(object? obj) => obj is IClothesTypeShortDomain clothesTypeDomain && Equals(clothesTypeDomain);

        public bool Equals(IClothesTypeShortDomain? other) =>
            other?.Id == Id &&
            other?.Category.Equals(Category) == true;

        public override int GetHashCode() => HashCode.Combine(Name, Category.GetHashCode());
        #endregion
    }
}