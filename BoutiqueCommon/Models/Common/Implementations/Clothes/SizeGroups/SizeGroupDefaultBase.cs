using System;
using System.Collections.Generic;
using System.Globalization;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes.SizeGroups
{
    /// <summary>
    /// Группа размеров одежды с размером по умолчанию. Базовые данные
    /// </summary>
    public abstract class SizeGroupDefaultBase<TSize> : SizeGroupMainBase<TSize>, ISizeGroupDefaultBase<TSize>
        where TSize : ISizeBase
    {
        protected SizeGroupDefaultBase(ClothesSizeType clothesSizeType, int sizeNormalize, IEnumerable<TSize> sizes,
                                       SizeType defaultSizeType)
            :base(clothesSizeType, sizeNormalize, sizes)
        {
            DefaultSizeType = defaultSizeType;
        }

        /// <summary>
        /// Тип размера по умолчанию
        /// </summary>
        public SizeType DefaultSizeType { get; }

        /// <summary>
        /// Имя с типом размера по умолчанию
        /// </summary>
        public string DefaultName =>
            GetBaseGroupName(DefaultSizeType);

        #region IFormattable Support
        public override string ToString() =>
            ToString(String.Empty, CultureInfo.CurrentCulture);

        public string ToString(string? format, IFormatProvider? formatProvider) =>
            DefaultName;
        #endregion
    }
}