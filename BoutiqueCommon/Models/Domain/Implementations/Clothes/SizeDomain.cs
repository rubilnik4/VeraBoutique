using System;
using System.Globalization;
using BoutiqueCommon.Infrastructure.Implementation;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes
{
    /// <summary>
    /// Размер одежды. Доменная модель
    /// </summary>
    public class SizeDomain : Size, ISizeDomain, IFormattable
    {
        public SizeDomain(SizeType sizeType, string sizeName)
            : base(sizeType, sizeName)
        { }

        /// <summary>
        /// Укороченное наименование размера
        /// </summary>
        public string SizeNameShort => SizeNaming.GetSizeNameShort(SizeType, SizeName);

        #region IFormattable Support
        public override string ToString() => ToString(String.Empty, CultureInfo.CurrentCulture);

        public string ToString(string? format, IFormatProvider? formatProvider) => SizeNameShort;
        #endregion

    }
}