using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using BoutiqueCommon.Infrastructure.Implementation;
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
        public Size(SizeType sizeType, int sizeValue)
            :this(sizeType, sizeValue, sizeValue.ToString())
        { }

        public Size(SizeType sizeType, int sizeValue, string sizeName)
        {
            SizeType = sizeType;
            SizeValue = sizeValue;
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
        public string ClothesSizeNameShort => SizeNaming.GetSizeNameShort(SizeType, SizeName);

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

        /// <summary>
        /// Получить хэш-код коллекции размеров одежды
        /// </summary>
        public static int GetSizesHashCodes<TSize>(IEnumerable<TSize> sizes)
            where TSize: ISize=>
            sizes.Aggregate(0, HashCode.Combine);
    }
}