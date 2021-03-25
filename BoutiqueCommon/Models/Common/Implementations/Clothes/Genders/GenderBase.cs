using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes.Genders
{
    /// <summary>
    /// Пол для одежды
    /// </summary>
    public abstract class GenderBase: IGenderBase
    {
        protected GenderBase(GenderType genderType, string name)
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
        public virtual GenderType GenderType { get; }

        /// <summary>
        /// Наименование
        /// </summary>
        public virtual string Name { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is IGenderBase gender && Equals(gender);

        public bool Equals(IGenderBase? other) =>
            other?.Id == Id;

        public override int GetHashCode() => HashCode.Combine(GenderType);
        #endregion
    }
}