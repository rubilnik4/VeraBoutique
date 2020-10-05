using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using Functional.FunctionalExtensions.Sync;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes
{
    /// <summary>
    /// Группа размеров одежды разного типа
    /// </summary>
    public class ClothesSizeGroup: IClothesSizeGroup, IEquatable<IClothesSizeGroup>, IFormattable
    {
        public ClothesSizeGroup(IClothesSize clothesSizeBase,
                                IReadOnlyCollection<IClothesSize> clothesSizesAdditional)
        {
            ClothesSizeBase = clothesSizeBase;
            ClothesSizesAdditional = clothesSizesAdditional;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id => Name;

        /// <summary>
        /// Размеры одежды
        /// </summary> ь  
        public IClothesSize ClothesSizeBase { get; }

        /// <summary>
        /// Размеры одежды
        /// </summary>
        public IReadOnlyCollection<IClothesSize> ClothesSizesAdditional { get; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name => ClothesSizeBase + GetClothesSizeGroupSubName();

        #region IEquatable
        public override bool Equals(object? obj) => obj is IClothesSizeGroup clothesSizeGroup && Equals(clothesSizeGroup);

        public bool Equals(IClothesSizeGroup? other) =>
            other?.Name == Name;

        public override int GetHashCode() => HashCode.Combine(Name);
        #endregion

        #region IFormattable Support
        public override string ToString() => ToString(String.Empty, CultureInfo.CurrentCulture);

        public string ToString(string? format, IFormatProvider? formatProvider) => Name;
        #endregion

        /// <summary>
        /// Получить наименование дополнительной группы размеров
        /// </summary>
        private string GetClothesSizeGroupSubName() =>
            ClothesSizesAdditional.
            WhereContinue(clothesSize => clothesSize.Count > 0,
                okFunc: clothesSizeCollection => $" ({String.Join(", ", clothesSizeCollection)})",
                badFunc: _ => String.Empty);
    }
}