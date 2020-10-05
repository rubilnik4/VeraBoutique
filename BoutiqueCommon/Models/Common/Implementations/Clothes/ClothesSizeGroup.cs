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
    /// Группа размеров одежды разной маркировки
    /// </summary>
    public class ClothesSizeGroup: IClothesSizeGroup, IEquatable<IClothesSizeGroup>, IFormattable
    {
        public ClothesSizeGroup(IReadOnlyCollection<ClothesSize> clothesSizes, 
                                ClothesSizeType clothesSizeBase)
        {
            ClothesSizes = clothesSizes;
            ClothesSizeBase = clothesSizeBase;
        }

        public string Id => Name;

        /// <summary>
        /// Размеры одежды
        /// </summary>
        public IReadOnlyCollection<ClothesSize> ClothesSizes { get; }

        /// <summary>
        /// Базовый тип размера группы
        /// </summary>
        public ClothesSizeType ClothesSizeBase { get; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name => GetClothesSizeGroupName(ClothesSizeBase, ClothesSizes);

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
        /// Получить наименование группы размеров
        /// </summary>
        public static string GetClothesSizeGroupName(ClothesSizeType clothesSizeBase,
                                                     IReadOnlyCollection<ClothesSize> clothesSizes) =>
            clothesSizes.FirstOrDefault(clothesSize => clothesSize.ClothesSizeType == clothesSizeBase) +
            GetClothesSizeGroupSubName(clothesSizeBase, clothesSizes);

        /// <summary>
        /// Получить наименование дополнительной группы размеров
        /// </summary>
        private static string GetClothesSizeGroupSubName(ClothesSizeType clothesSizeBase,
                                                         IEnumerable<ClothesSize> clothesSizes) =>
            clothesSizes.
            Where(clothesSize => clothesSize.ClothesSizeType != clothesSizeBase).
            ToList().
            WhereContinue(clothesSizeCollection => clothesSizeCollection.Count > 0,
                okFunc: clothesSizeCollection => $" ({String.Join(",", clothesSizeCollection)})",
                badFunc: _ => string.Empty);
    }
}