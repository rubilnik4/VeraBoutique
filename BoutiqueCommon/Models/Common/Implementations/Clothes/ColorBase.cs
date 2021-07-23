using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes
{
    /// <summary>
    /// Цвет одежды
    /// </summary>
    public abstract class ColorBase : IColorBase
    {
        protected ColorBase(string name)
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
        public override bool Equals(object? obj) => obj is IColorBase color && Equals(color);

        public bool Equals(IColorBase? other) =>
            other?.Id == Id;

        public override int GetHashCode() => HashCode.Combine(Id);
        #endregion

        #region IFormattable Support
        public override string ToString() => 
            ToString(String.Empty, CultureInfo.CurrentCulture);

        public string ToString(string? format, IFormatProvider? formatProvider) =>
            Name;
        #endregion
    }
}