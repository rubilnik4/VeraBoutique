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
    public class SizeGroup<TSize>: ISizeGroup<TSize>, IEquatable<ISizeGroup<TSize>>
        where TSize: ISize
    {
        public SizeGroup(ClothesSizeType clothesSizeType, int sizeNormalize,
                         IEnumerable<TSize> sizes)
        {
            ClothesSizeType = clothesSizeType;
            SizeNormalize = sizeNormalize;
            Sizes = sizes.ToList();
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
        /// Дополнительные размеры одежды
        /// </summary>
        public IReadOnlyCollection<TSize> Sizes { get; }

        /// <summary>
        /// Получить имя группы размеров по базовому типу
        /// </summary>
        public string GetBaseGroupName(SizeType sizeType) =>
            SizeNaming.GetGroupName(sizeType, Sizes);

        #region IEquatable
        public override bool Equals(object? obj) => obj is ISizeGroup<TSize> clothesSizeGroup && Equals(clothesSizeGroup);

        public bool Equals(ISizeGroup<TSize>? other) =>
            other?.Id == Id && Sizes.SequenceEqual(other?.Sizes);

        public override int GetHashCode() => HashCode.Combine(ClothesSizeType, SizeNormalize, Size.GetSizesHashCodes(Sizes));
        #endregion
    }
}