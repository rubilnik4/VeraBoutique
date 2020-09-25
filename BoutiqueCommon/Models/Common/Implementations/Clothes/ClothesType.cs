using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes
{
    /// <summary>
    /// Вид одежды
    /// </summary>
    public abstract class ClothesType: IClothesType, IEquatable<IClothesType>
    {
        protected ClothesType(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id => Name;

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is IClothesType clothesType && Equals(clothesType);

        public bool Equals(IClothesType? other) =>
            other?.Name == Name ;

        public override int GetHashCode() => HashCode.Combine(Name);
        #endregion
    }
}