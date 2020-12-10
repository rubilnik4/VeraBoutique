using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes.ClothesTypes
{
    public abstract class ClothesTypeBase<TCategory, TGender> : ClothesTypeShortBase, IClothesTypeBase<TCategory, TGender>
        where TCategory : ICategoryBase
        where TGender : IGenderBase
    {
        protected ClothesTypeBase(string name, TCategory category, IEnumerable<TGender> genders)
            :base(name, category.Name)
        {
            Category = category;
            Genders = genders.ToList();
        }

        /// <summary>
        /// Категория одежды
        /// </summary>
        public TCategory Category { get; }

        /// <summary>
        /// Типы пола
        /// </summary>
        public IReadOnlyCollection<TGender> Genders { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is IClothesTypeBase<TCategory, TGender> clothesType && Equals(clothesType);

        public bool Equals(IClothesTypeBase<TCategory, TGender>? other) =>
            base.Equals(other) &&
            other?.Category.Equals(Category) == true &&
            other?.Genders.SequenceEqual(Genders) == true;

        public override int GetHashCode() => HashCode.Combine(Name, Category.GetHashCode(), GenderBase.GetGendersHashCodes(Genders));
        #endregion
    }
}