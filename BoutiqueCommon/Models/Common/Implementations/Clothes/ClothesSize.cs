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
    public class ClothesSize<TSize> : IClothesSize<TSize>, IEquatable<IClothesSize<TSize>>, IFormattable
        where TSize : IEquatable<TSize>
    {
        public ClothesSize(ClothesSizeType clothesSizeType, TSize size, string sizeName)
        {
            ClothesSizeType = clothesSizeType;
            Size = size;
            SizeName = sizeName;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id => 
            $"{ClothesSizeTypeShort(ClothesSizeType)} {SizeName}".
            Trim();

        /// <summary>
        /// Тип размера одежды
        /// </summary>
        public ClothesSizeType ClothesSizeType { get; }

        /// <summary>
        /// Размер
        /// </summary>
        public TSize Size { get; }

        /// <summary>
        /// Размер
        /// </summary>
        public string SizeName { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is IClothesSize<TSize> clothesSize && Equals(clothesSize);

        public bool Equals(IClothesSize<TSize>? other) =>
            other?.ClothesSizeType == ClothesSizeType &&
            other?.Size.Equals(Size) == true;

        public override int GetHashCode() => HashCode.Combine(ClothesSizeType, Size);
        #endregion

        #region IFormattable Support
        public override string ToString() => ToString(String.Empty, CultureInfo.CurrentCulture);

        public string ToString(string? format, IFormatProvider? formatProvider) => Id;
        #endregion

        /// <summary>
        /// Укороченное наименование типа размера
        /// </summary>
        public static string ClothesSizeTypeShort(ClothesSizeType clothesSizeType) =>
            clothesSizeType switch
            {
                ClothesSizeType.American => "",
                ClothesSizeType.European => "EU",
                ClothesSizeType.Russian => "RU",
                _ => throw new InvalidEnumArgumentException(nameof(clothesSizeType), (int)clothesSizeType, typeof(ClothesSizeType))
            };
    }
}