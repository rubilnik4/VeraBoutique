using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes
{
    /// <summary>
    /// Одежда. Информация
    /// </summary>
    public abstract class ClothesInformation : ClothesShort, IClothesInformation, IEquatable<IClothesInformation>
    {
        protected ClothesInformation(int id, string name, string description, decimal price, byte[]? image)
            :base(id, name, price, image)
        {
            Description = description;
        }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is IClothesInformation clothes && Equals(clothes);

        public bool Equals(IClothesInformation? other) =>
            other?.Id == Id && other?.Name == Name && other?.Price == Price;

        public override int GetHashCode() => HashCode.Combine(Id, Name, Price, Description);
        #endregion
    }
}