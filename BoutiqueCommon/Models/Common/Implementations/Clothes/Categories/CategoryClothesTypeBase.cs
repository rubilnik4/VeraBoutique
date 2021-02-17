using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes.Categories
{
    /// <summary>
    /// Категория одежды с подтипами
    /// </summary>
    public class CategoryClothesTypeBase<TClothesType> : CategoryBase, ICategoryClothesTypeBase<TClothesType>
        where TClothesType : IClothesTypeBase
    {
        public CategoryClothesTypeBase(ICategoryBase category, IEnumerable<TClothesType> clothesTypes)
            :this(category.Name, clothesTypes)
        { }

        public CategoryClothesTypeBase(string name, IEnumerable<TClothesType> clothesTypes)
            : base(name)
        {
            ClothesTypes = clothesTypes.ToList().AsReadOnly();
        }

        /// <summary>
        /// Типы одежды
        /// </summary>
        public IReadOnlyCollection<TClothesType> ClothesTypes { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is ICategoryClothesTypeBase<TClothesType> categoryClothesType && 
                                                    Equals(categoryClothesType);

        public bool Equals(ICategoryClothesTypeBase<TClothesType>? other) =>
            other?.Id == Id &&
            other?.ClothesTypes.SequenceEqual(ClothesTypes) == true;

        public override int GetHashCode() => HashCode.Combine(Name, ClothesTypeBase.GetClothesTypeHashCodes(ClothesTypes));
        #endregion
    }
}