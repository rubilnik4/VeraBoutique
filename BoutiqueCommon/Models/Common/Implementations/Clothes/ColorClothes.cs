using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes
{
    /// <summary>
    /// Цвет одежды
    /// </summary>
    public abstract class ColorClothes : IColorClothes
    {
        protected ColorClothes(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id => Name;

        /// <summary>
        /// Тип размера одежды
        /// </summary>
        public string Name { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is IColorClothes color && Equals(color);

        public bool Equals(IColorClothes? other) =>
            other?.Id == Id;

        public override int GetHashCode() => HashCode.Combine(Name);
        #endregion
    }
}