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
    public abstract class ColorBase : IColorBase, IEquatable<IColorBase>
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

        public override int GetHashCode() => HashCode.Combine(Name);
        #endregion

        /// <summary>
        /// Получить хэш-код коллекции цветов одежды
        /// </summary>
        public static double GetColorClothesHashCodes<TColor>(IEnumerable<TColor> colors)
            where TColor : IColorBase =>
            colors.Average(color => color.GetHashCode());
    }
}