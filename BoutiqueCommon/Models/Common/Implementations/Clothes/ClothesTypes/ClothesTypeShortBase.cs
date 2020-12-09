using System;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes.ClothesTypes
{
    /// <summary>
    /// Вид одежды. Базовые данные
    /// </summary>
    public abstract class ClothesTypeShortBase: IClothesTypeShortBase, IEquatable<IClothesTypeShortBase>
    {
        protected ClothesTypeShortBase(string name, string categoryName)
        {
            Name = name;
            CategoryName = categoryName;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id => Name;

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Категория
        /// </summary>
        public string CategoryName { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is IClothesTypeShortBase clothesType && Equals(clothesType);

        public bool Equals(IClothesTypeShortBase? other) =>
            other?.Id == Id &&
            other?.CategoryName.Equals(CategoryName) == true;

        public override int GetHashCode() => HashCode.Combine(Name, CategoryName);
        #endregion
    }
}