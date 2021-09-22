using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using BoutiqueCommon.Infrastructure.Implementation;
using BoutiqueCommon.Infrastructure.Implementation.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes
{
    /// <summary>
    /// Размер одежды
    /// </summary>
    public abstract class SizeBase : ISizeBase
    {
        protected SizeBase(SizeType sizeType, string name)
        {
            SizeType = sizeType;
            Name = name;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public abstract int Id { get; }

        /// <summary>
        /// Тип размера одежды
        /// </summary>
        public SizeType SizeType { get; }

        /// <summary>
        /// Размер
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Укороченное наименование размера
        /// </summary>
        public string SizeNameShort => SizeNaming.GetSizeNameShort(SizeType, Name);

        #region IEquatable
        public override bool Equals(object? obj) =>
            obj is ISizeBase clothesSize && Equals(clothesSize);

        public bool Equals(ISizeBase? other) =>
            other?.Id == Id;

        public override int GetHashCode() => 
            GetIdHashCode(SizeType, Name);
        #endregion

        #region IFormattable Support
        public override string ToString() => ToString(String.Empty, CultureInfo.CurrentCulture);

        public string ToString(string? format, IFormatProvider? formatProvider) => SizeNameShort;
        #endregion

        /// <summary>
        /// Получить хэш-код идентификатора
        /// </summary>
        public static int GetIdHashCode(SizeType sizeType, string name) =>
            HashCode.Combine(sizeType, name);
    }
}