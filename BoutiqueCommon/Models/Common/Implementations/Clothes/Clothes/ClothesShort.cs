using System;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes.Clothes
{
    /// <summary>
    /// Одежда
    /// </summary>
    public abstract class ClothesShort : IClothesShort, IEquatable<IClothesShort>
    {
        protected ClothesShort(int id, string name, decimal price, byte[]? image)
        {
            Id = id;
            Name = name;
            Price = price;
            Image = image;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; }

        /// <summary>
        /// Изображение
        /// </summary>
        public byte[]? Image { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is IClothesShort clothes && Equals(clothes);

        public bool Equals(IClothesShort? other) =>
            other?.Id == Id && other?.Name == Name && other?.Price == Price;

        public override int GetHashCode() => HashCode.Combine(Id, Name, Price);
        #endregion
    }
}