using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using BoutiqueCommon.Infrastructure.Implementation;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups;
using BoutiqueCommon.Models.Enums.Clothes;
using Functional.FunctionalExtensions.Sync;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes.SizeGroups
{
    /// <summary>
    /// Группа размеров одежды. Базовые данные
    /// </summary>
    public abstract class SizeGroupShortBase : ISizeGroupShortBase
    {
        protected SizeGroupShortBase(ClothesSizeType clothesSizeType, int sizeNormalize)
        {
            ClothesSizeType = clothesSizeType;
            SizeNormalize = sizeNormalize;
        }

        /// <summary>
        /// Минимальное значение номинального размера
        /// </summary>
        public const int SIZE_NORMALIZE_MIN = 1;

        /// <summary>
        /// Максимальное значение номинального размера
        /// </summary>
        public const int SIZE_NORMALIZE_MAX = 299;

        /// <summary>
        /// Идентификатор
        /// </summary>
        public abstract int Id { get; }

        /// <summary>
        /// Тип одежды для определения размера
        /// </summary>
        public ClothesSizeType ClothesSizeType { get; }

        /// <summary>
        /// Номинальное значение размера
        /// </summary>
        public int SizeNormalize { get; }

        #region IEquatable
        public override bool Equals(object? obj) =>
            obj is ISizeGroupShortBase sizeGroup && Equals(sizeGroup);

        public bool Equals(ISizeGroupShortBase? other) =>
            other?.Id.Equals(Id) == true;

        public override int GetHashCode() =>
            GetIdHashCode(ClothesSizeType, SizeNormalize);
        #endregion

        /// <summary>
        /// Получить хэш-код идентификатора
        /// </summary>
        public static int GetIdHashCode(ClothesSizeType clothesSizeType, int sizeNormalize) =>
            HashCode.Combine(clothesSizeType, sizeNormalize);
    }
}