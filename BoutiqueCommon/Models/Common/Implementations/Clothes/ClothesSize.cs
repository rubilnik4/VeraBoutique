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
    public class ClothesSize: IClothesSize, IEquatable<IClothesSize>, IFormattable
    {
        public ClothesSize(ClothesSizeType clothesSizeType, int size, string sizeName)
        {
            ClothesSizeType = clothesSizeType;
            Size = size;
            SizeName = sizeName;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id => ClothesSizeNameShort;

        /// <summary>
        /// Тип размера одежды
        /// </summary>
        public ClothesSizeType ClothesSizeType { get; }

        /// <summary>
        /// Размер
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Размер
        /// </summary>
        public string SizeName { get; }

        /// <summary>
        /// Укороченное наименование размера
        /// </summary>
        private string ClothesSizeNameShort => $"{ClothesSizeTypeShort} {SizeName}".Trim();

        /// <summary>
        /// Укороченное наименование типа размера
        /// </summary>
        private string ClothesSizeTypeShort =>
            ClothesSizeType switch
            {
                ClothesSizeType.American => "",
                ClothesSizeType.European => "EU",
                ClothesSizeType.Russian => "RU",
                _ => throw new InvalidEnumArgumentException(nameof(ClothesSizeType), (int)ClothesSizeType, typeof(ClothesSizeType))
            };

        #region IEquatable
        public override bool Equals(object? obj) => obj is IClothesSize clothesSize && Equals(clothesSize);

        public bool Equals(IClothesSize? other) =>
            other?.Id == Id;

        public override int GetHashCode() => HashCode.Combine(ClothesSizeType, Size);
        #endregion

        #region IFormattable Support
        public override string ToString() => ToString(String.Empty, CultureInfo.CurrentCulture);

        public string ToString(string? format, IFormatProvider? formatProvider) => ClothesSizeNameShort;
        #endregion
    }
}