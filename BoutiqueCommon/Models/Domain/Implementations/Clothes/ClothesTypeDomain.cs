using System;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesType;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes
{
    /// <summary>
    /// Вид одежды. Доменная модель
    /// </summary>
    public class ClothesTypeDomain: ClothesTypeShortDomain, IClothesTypeDomain
    {
        public ClothesTypeDomain(string name, IGenderDomain genderDomain , ICategoryDomain categoryDomain)
        : base(name)
        {
            GenderDomain = genderDomain;
            CategoryDomain = categoryDomain;
        }

        /// <summary>
        /// Тип пола. Доменная модель
        /// </summary>
        public IGenderDomain GenderDomain { get; }

        /// <summary>
        /// Категория одежды. Доменная модель
        /// </summary>
        public ICategoryDomain CategoryDomain { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is IClothesTypeDomain clothesTypeDomain && Equals(clothesTypeDomain);

        public bool Equals(IClothesTypeDomain? other) =>
            other?.Id == Id &&
            other?.GenderDomain.Equals(GenderDomain) == true &&
            other?.CategoryDomain.Equals(CategoryDomain) == true;

        public override int GetHashCode() => HashCode.Combine(Name, GenderDomain.GetHashCode(), CategoryDomain.GetHashCode());
        #endregion
    }
}