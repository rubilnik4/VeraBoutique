﻿using System;
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
        public (ClothesSizeType, int) Id => (ClothesSizeType, SizeNormalize);

        /// <summary>
        /// Тип одежды для определения размера
        /// </summary>
        public ClothesSizeType ClothesSizeType { get; }

        /// <summary>
        /// Номинальное значение размера
        /// </summary>
        public int SizeNormalize { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is ISizeGroup sizeGroup && Equals(sizeGroup);

        public bool Equals(ISizeGroup? other) =>
            other?.Id == Id;

        public override int GetHashCode() => HashCode.Combine(ClothesSizeType, SizeNormalize);
        #endregion
    }
}