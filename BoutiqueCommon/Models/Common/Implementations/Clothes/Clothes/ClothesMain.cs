using System;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes.Clothes
{
    /// <summary>
    /// Одежда. Информация
    /// </summary>
    public abstract class ClothesMain : ClothesShort, IClothesMain, IEquatable<IClothesMain>
    {
        protected ClothesMain(int id, string name, string description, decimal price, byte[]? image)
            :base(id, name, price, image)
        {
            Description = description;
        }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is IClothesMain clothes && Equals(clothes);

        public bool Equals(IClothesMain? other) =>
            other?.Id == Id && other?.Name == Name && other?.Price == Price;

        public override int GetHashCode() => HashCode.Combine(Id, Name, Price, Description);
        #endregion
    }
}