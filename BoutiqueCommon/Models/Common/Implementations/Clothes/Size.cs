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
    public abstract class Size : ISize
    {
        protected Size(SizeType sizeType, string sizeName)
        {
            SizeType = sizeType;
            SizeName = sizeName;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public (SizeType, string) Id => (SizeType, SizeName);

        /// <summary>
        /// Тип размера одежды
        /// </summary>
        public SizeType SizeType { get; }

        /// <summary>
        /// Размер
        /// </summary>
        public string SizeName { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is ISize clothesSize && Equals(clothesSize);

        public bool Equals(ISize? other) =>
            other?.Id == Id;

        public override int GetHashCode() => HashCode.Combine(SizeType, SizeName);
        #endregion

        /// <summary>
        /// Получить хэш-код коллекции размеров одежды
        /// </summary>
        public static double GetSizesHashCodes<TSize>(IEnumerable<TSize> sizes)
            where TSize: ISize=>
            sizes.Average(size => size.GetHashCode());
    }
}