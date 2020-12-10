using System;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes.Clothes
{
    /// <summary>
    /// Одежда. Базовые данные
    /// </summary>
    public abstract class ClothesShortBase : IClothesShortBase
    {
        protected ClothesShortBase(int id, string name, string description, decimal price, byte[]? image, 
                                   GenderType genderType, string clothesTypeName)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Image = image;
            GenderType = genderType;
            ClothesTypeName = clothesTypeName;
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

        /// <summary>
        /// Тип пола одежды
        /// </summary>
        public GenderType GenderType { get; }

        /// <summary>
        /// Тип одежды
        /// </summary>
        public string ClothesTypeName { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is IClothesShortBase clothes && Equals(clothes);

        public bool Equals(IClothesShortBase? other) =>
            other?.Id == Id && 
            other?.Name == Name && 
            other?.Description == Description && 
            other?.Price == Price &&
            GenderType == other.GenderType &&
            ClothesTypeName == other.ClothesTypeName;

        public override int GetHashCode() => HashCode.Combine(Id, Name, Description, Price, GenderType, ClothesTypeName);
        #endregion
    }
}