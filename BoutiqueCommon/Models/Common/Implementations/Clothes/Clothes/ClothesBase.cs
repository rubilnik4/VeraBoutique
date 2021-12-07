using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes.Clothes
{
    /// <summary>
    /// Одежда. Базовые данные
    /// </summary>
    public abstract class ClothesBase : IClothesBase
    {
        protected ClothesBase(int id, string name, string description, decimal price, GenderType genderType, string clothesTypeName)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
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
        /// Тип пола одежды
        /// </summary>
        public GenderType GenderType { get; }

        /// <summary>
        /// Тип одежды
        /// </summary>
        public string ClothesTypeName { get; }

        #region IEquatable
        public override bool Equals(object? obj) => 
            obj is IClothesBase clothes && Equals(clothes);

        public bool Equals(IClothesBase? other) =>
            other?.Id == Id;

        public override int GetHashCode() =>
            HashCode.Combine(Id);
        #endregion
    }
}