using System;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes.ClothesTypes
{
    public abstract class ClothesTypeMainBase<TCategory> : ClothesTypeBase, IClothesTypeMainBase<TCategory>
        where TCategory : ICategoryBase
    {
        protected ClothesTypeMainBase(string name, TCategory category)
            :base(name, category.Name)
        {
            Category = category;
        }

        /// <summary>
        /// Категория одежды
        /// </summary>
        public TCategory Category { get; }

        #region IEquatable
        public override bool Equals(object? obj) =>
            obj is IClothesTypeMainBase<TCategory> clothesType && Equals(clothesType);

        public bool Equals(IClothesTypeMainBase<TCategory>? other) =>
            other?.Id == Id &&
            other?.Category.Equals(Category) == true;

        public override int GetHashCode() => 
            HashCode.Combine(Name, Category);
        #endregion
    }
}