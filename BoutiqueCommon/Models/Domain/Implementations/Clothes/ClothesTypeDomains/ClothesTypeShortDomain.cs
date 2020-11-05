using System;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains
{
    /// <summary>
    /// Вид одежды. Основная информация. Доменная модель
    /// </summary>
    public class ClothesTypeShortDomain : ClothesTypeDomain, IClothesTypeShortDomain, IEquatable<IClothesTypeShortDomain>
    {
        public ClothesTypeShortDomain(IClothesType clothesType, ICategoryDomain category, IGenderDomain gender)
            : this(clothesType.Name, category, gender)
        { }

        public ClothesTypeShortDomain(string name, ICategoryDomain category, IGenderDomain gender)
          : base(name, category)
        {
            Gender = gender;
        }

        /// <summary>
        /// Тип пола. Доменная модель
        /// </summary>
        public IGenderDomain Gender { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is IClothesTypeShortDomain clothesTypeDomain && Equals(clothesTypeDomain);

        public bool Equals(IClothesTypeShortDomain? other) =>
            other?.Id == Id &&
            other?.Category.Equals(Category) == true &&
            other?.Gender.Equals(Gender) == true;

        public override int GetHashCode() => HashCode.Combine(Name, Category.GetHashCode(), Gender.GetHashCode());
        #endregion
    }
}