﻿using System;
using System.Linq.Expressions;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes
{
    /// <summary>
    /// Пол для одежды
    /// </summary>
    public abstract class Gender: IGender, IEquatable<IGender>
    {
        protected Gender(GenderType genderType, string name)
        {
            GenderType = genderType;
            Name = name;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public GenderType Id => GenderType;

        /// <summary>
        /// Тип пола
        /// </summary>
        public GenderType GenderType { get; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is IGender gender && Equals(gender);

        public bool Equals(IGender? other) =>
            other?.GenderType == GenderType;

        public override int GetHashCode() => HashCode.Combine(GenderType);
        #endregion
    }
}