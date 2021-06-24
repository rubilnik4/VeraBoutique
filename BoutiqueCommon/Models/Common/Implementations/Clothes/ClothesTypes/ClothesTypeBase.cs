using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes.ClothesTypes
{
    /// <summary>
    /// Вид одежды. Базовые данные
    /// </summary>
    public abstract class ClothesTypeBase: IClothesTypeBase
    {
        protected ClothesTypeBase(string name, SizeType sizeTypeDefault, string categoryName)
        {
            Name = name;
            SizeTypeDefault = sizeTypeDefault;
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
        /// Тип размера по умолчанию
        /// </summary>
        public SizeType SizeTypeDefault { get; }

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