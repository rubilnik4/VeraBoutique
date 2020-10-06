using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using Functional.FunctionalExtensions.Sync;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes
{
    /// <summary>
    /// Группа размеров одежды разного типа
    /// </summary>
    public class SizeGroup: ISizeGroup, IEquatable<ISizeGroup>
    {
        public SizeGroup(ClothesSizeType clothesSizeType, int sizeNormalize,
                         IReadOnlyCollection<ISize> sizes)
        {
            ClothesSizeType = clothesSizeType;
            SizeNormalize = sizeNormalize;
            Sizes = sizes;
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
        public IReadOnlyCollection<ISize> Sizes { get; }

        /// <summary>
        /// Получить имя группы размеров по базовому типу
        /// </summary>
        public string GetBaseGroupName(SizeType sizeType) =>
            GetGroupName(Sizes.FirstOrDefault(size => size.SizeType == sizeType),
                         Sizes.Where(size => size.SizeType != sizeType));

        #region IEquatable
        public override bool Equals(object? obj) => obj is ISizeGroup clothesSizeGroup && Equals(clothesSizeGroup);

        public bool Equals(ISizeGroup? other) =>
            other?.Id == Id;

        public override int GetHashCode() => HashCode.Combine(ClothesSizeType, SizeNormalize);
        #endregion

        /// <summary>
        /// Получить имя группы
        /// </summary>
        public static string GetGroupName(ISize? clothesSizeBase ,
                                          IEnumerable<ISize> clothesSizesAdditional) =>
            (clothesSizeBase?.ToString() ?? String.Empty) + 
            GetClothesSizeGroupSubName(clothesSizesAdditional);

        /// <summary>
        /// Получить наименование дополнительной группы размеров
        /// </summary>
        private static string GetClothesSizeGroupSubName(IEnumerable<ISize> clothesSizesAdditional) =>
            clothesSizesAdditional.ToList().
            WhereContinue(clothesSize => clothesSize.Count > 0,
                okFunc: clothesSizeCollection => $" ({String.Join(", ", clothesSizeCollection)})",
                badFunc: _ => String.Empty);
    }
}