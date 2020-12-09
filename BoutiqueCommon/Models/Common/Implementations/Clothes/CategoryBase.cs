using System;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes
{
    /// <summary>
    /// Категория одежды
    /// </summary>
    public abstract class CategoryBase : ICategoryBase, IEquatable<ICategoryBase>
    {
        protected CategoryBase(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id => Name;

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is ICategoryBase category && Equals(category);

        public bool Equals(ICategoryBase? other) =>
            other?.Id == Id;

        public override int GetHashCode() => HashCode.Combine(Name);
        #endregion
    }
}