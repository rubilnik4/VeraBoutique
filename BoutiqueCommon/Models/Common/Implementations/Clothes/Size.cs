using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes
{
    /// <summary>
    /// Размер одежды
    /// </summary>
    public class Size : ISize, IEquatable<ISize>, IFormattable
    {
        public Size(SizeType clothesSizeType, int size, string sizeName)
        {
            SizeType = clothesSizeType;
            SizeValue = size;
            SizeName = sizeName;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public (SizeType, int) Id => (SizeType, SizeValue);

        /// <summary>
        /// Тип размера одежды
        /// </summary>
        public SizeType SizeType { get; }

        /// <summary>
        /// Размер
        /// </summary>
        public int SizeValue { get; }

        /// <summary>
        /// Размер
        /// </summary>
        public string SizeName { get; }

        /// <summary>
        /// Укороченное наименование размера
        /// </summary>
        private string ClothesSizeNameShort => GetClothesSizeNameShort(SizeType, SizeName);

         /// <summary>
         /// Получить укороченное наименование размера
         /// </summary>
        public static string GetClothesSizeNameShort(SizeType clothesSizeType, string sizeName) =>
             $"{GetClothesSizeTypeShort(clothesSizeType)} {sizeName}".Trim();

        /// <summary>
        /// Укороченное наименование типа размера
        /// </summary>
        private static string GetClothesSizeTypeShort(SizeType clothesSizeType) =>
            clothesSizeType switch
            {
                SizeType.American => "",
                SizeType.European => "EU",
                SizeType.Russian => "RU",
                _ => throw new InvalidEnumArgumentException(nameof(SizeType), (int)clothesSizeType, typeof(SizeType))
            };

        #region IEquatable
        public override bool Equals(object? obj) => obj is ISize clothesSize && Equals(clothesSize);

        public bool Equals(ISize? other) =>
            other?.Id == Id;

        public override int GetHashCode() => HashCode.Combine(SizeType, SizeValue);
        #endregion

        #region IFormattable Support
        public override string ToString() => ToString(String.Empty, CultureInfo.CurrentCulture);

        public string ToString(string? format, IFormatProvider? formatProvider) => ClothesSizeNameShort;
        #endregion
    }
}