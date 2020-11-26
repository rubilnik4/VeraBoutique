using System;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes
{
    /// <summary>
    /// Одежда
    /// </summary>
    public abstract class ClothesMain : IClothesMain, IEquatable<IClothesMain>
    {
        protected ClothesMain(int id, string name, string description, decimal price, byte[]? image)
        {
            Id = id;
            Name = name;
            Description = description;
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
        /// Описание
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; }

        /// <summary>
        /// Изображение
        /// </summary>
        public byte[]? Image { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is IClothesMain clothes && Equals(clothes);

        public bool Equals(IClothesMain? other) =>
            other?.Id == Id && other?.Name == Name && other?.Description == Description && other?.Price == Price;

        public override int GetHashCode() => HashCode.Combine(Id, Name, Description, Price);
        #endregion
    }
}