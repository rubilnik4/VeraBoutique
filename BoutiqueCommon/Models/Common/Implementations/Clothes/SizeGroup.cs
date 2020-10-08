using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using BoutiqueCommon.Infrastructure.Implementation;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using Functional.FunctionalExtensions.Sync;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes
{
    /// <summary>
    /// Группа размеров одежды разного типа
    /// </summary>
    public abstract class SizeGroup: ISizeGroup, IEquatable<ISizeGroup>
    {
        protected SizeGroup(ClothesSizeType clothesSizeType, int sizeNormalize)
        {
            ClothesSizeType = clothesSizeType;
            SizeNormalize = sizeNormalize;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public (ClothesSizeType, int) Id => (ClothesSizeType, SizeNormalize);

        /// <summary>
        /// Тип одежды для определения размера
        /// </summary>
        public ClothesSizeType ClothesSizeType { get; }

        /// <summary>
        /// Номинальное значение размера
        /// </summary>
        public int SizeNormalize { get; }

        /// <summary>
        /// Получить имя группы размеров по базовому типу
        /// </summary>
        public string GetBaseGroupName(SizeType sizeType) =>
            SizeNaming.GetGroupName(sizeType, Sizes);

        #region IEquatable
        public override bool Equals(object? obj) => obj is ISizeGroup sizeGroup && Equals(sizeGroup);

        public bool Equals(ISizeGroup<TSize>? other) =>
            other?.Id == Id && Sizes.SequenceEqual(other?.Sizes);

        public override int GetHashCode() => HashCode.Combine(ClothesSizeType, SizeNormalize, Size.GetSizesHashCodes(Sizes));
        #endregion
    }
}