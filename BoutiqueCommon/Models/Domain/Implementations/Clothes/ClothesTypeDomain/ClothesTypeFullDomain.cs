using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomain;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomain
{
    /// <summary>
    /// Вид одежды. Полная информация. Доменная модель
    /// </summary>
    public class ClothesTypeFullDomain : ClothesTypeDomain, IClothesTypeFullDomain, IEquatable<IClothesTypeFullDomain>
    {
        public ClothesTypeFullDomain(IClothesType clothesType, ICategoryDomain category,
                                     IEnumerable<IGenderDomain> genders)
            : this(clothesType.Name, category, genders)
        { }

        public ClothesTypeFullDomain(string name, ICategoryDomain category, IEnumerable<IGenderDomain> genders)
        : base(name, category)
        {
            Genders = genders.ToList();
        }

        /// <summary>
        /// Типы пола. Доменная модель
        /// </summary>
        public IReadOnlyCollection<IGenderDomain> Genders { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is IClothesTypeFullDomain clothesTypeDomain && Equals(clothesTypeDomain);

        public bool Equals(IClothesTypeFullDomain? other) =>
            other?.Id == Id &&
            other?.Category.Equals(Category) == true &&
            other?.Genders.SequenceEqual(Genders) == true;

        public override int GetHashCode() => HashCode.Combine(Name, Category.GetHashCode(),
                                                              Gender.GetGendersHashCodes(Genders));
        #endregion
    }
}