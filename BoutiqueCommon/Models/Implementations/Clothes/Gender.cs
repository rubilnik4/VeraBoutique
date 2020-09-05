using System;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommon.Models.Interfaces.Base;
using BoutiqueCommon.Models.Interfaces.Clothes;

namespace BoutiqueCommon.Models.Implementations.Clothes
{
    /// <summary>
    /// Пол для одежды
    /// </summary>
    public class Gender : IGender, IEquatable<Gender>
    {
        public Gender(GenderType genderType, string name)
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
        public override bool Equals(object? obj) => obj is Gender gender && Equals(gender);

        public bool Equals(Gender? other) => 
            other?.GenderType == GenderType &&
            other?.Name == Name;

        public override int GetHashCode() => HashCode.Combine(GenderType, Name);
        #endregion
    }
}