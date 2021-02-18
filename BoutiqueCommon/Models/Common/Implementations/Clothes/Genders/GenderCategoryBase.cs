using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes.Categories;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes.Genders
{
    /// <summary>
    /// Тип пола с категориями
    /// </summary>
    public abstract class GenderCategoryBase<TCategory, TClothesType> : GenderBase, IGenderCategoryBase<TCategory, TClothesType>
        where TCategory : ICategoryClothesTypeBase<TClothesType>
        where TClothesType : IClothesTypeBase
    {
        protected GenderCategoryBase(IGenderBase gender, IEnumerable<TCategory> categories)
            :this(gender.GenderType, gender.Name, categories)
        { }

        protected GenderCategoryBase(GenderType genderType, string name, IEnumerable<TCategory> categories)
            : base(genderType, name)
        {
            Categories = categories.ToList().AsReadOnly();
        }

        /// <summary>
        /// Категории одежды
        /// </summary>
        public IReadOnlyCollection<TCategory> Categories { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is IGenderCategoryBase<TCategory, TClothesType> genderCategorized && 
                                                    Equals(genderCategorized);

        public bool Equals(IGenderCategoryBase<TCategory, TClothesType>? other) =>
            other?.Id == Id &&
            other?.Categories.SequenceEqual(Categories) == true;

        public override int GetHashCode() => HashCode.Combine(GenderType, CategoryBase.GetCategoriesHashCodes(Categories));
        #endregion
    }
}