using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Infrastructure.Implementation.Errors;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains
{
    /// <summary>
    /// Вид одежды. Доменная модель
    /// </summary>
    public class ClothesTypeDomain : ClothesTypeShortDomain, IClothesTypeDomain
    {
        public ClothesTypeDomain(IClothesType clothesType, ICategoryDomain category, IEnumerable<IGenderDomain> genders)
            : this(clothesType.Name, category, genders)
        { }

        public ClothesTypeDomain(string name, ICategoryDomain category, IEnumerable<IGenderDomain> genders)
            : base(name, category.Name)
        {
            Category = category;
            Genders = genders.ToList();
        }

        /// <summary>
        /// Категория одежды. Доменная модель
        /// </summary>
        public ICategoryDomain Category { get; }

        /// <summary>
        /// Типы пола. Доменная модель
        /// </summary>
        public IReadOnlyCollection<IGenderDomain> Genders { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is IClothesTypeDomain clothesTypeDomain && Equals(clothesTypeDomain);

        public bool Equals(IClothesTypeDomain? other) =>
            ((IClothesTypeShortDomain?)other)?.Equals(this) == true &&
            other?.Category.Equals(Category) == true &&
            other?.Genders.SequenceEqual(Genders) == true;

        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), Category.GetHashCode(),
                                                              Gender.GetGendersHashCodes(Genders));
        #endregion
    }
}