using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes.ClothesTypes
{
    /// <summary>
    /// Вид одежды. Базовые данные
    /// </summary>
    public abstract class ClothesTypeBase: IClothesTypeBase
    {
        protected ClothesTypeBase(string name, string categoryName)
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
        public override bool Equals(object? obj) => 
            obj is IClothesTypeBase clothesType && Equals(clothesType);

        public bool Equals(IClothesTypeBase? other) =>
            other?.Id == Id &&
            other?.CategoryName.Equals(CategoryName) == true;

        public override int GetHashCode() => 
            HashCode.Combine(Name, CategoryName);
        #endregion
    }
}