using System;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Images;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes.Images
{
    /// <summary>
    /// Изображение
    /// </summary>
    public abstract class ClothesImageBase : IClothesImageBase
    {
        protected ClothesImageBase(int id, byte[] image, bool isMain)
        {
            Id = id;
            Image = image;
            IsMain = isMain;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Изображение
        /// </summary>
        public byte[] Image { get; }

        /// <summary>
        /// Главное изображение
        /// </summary>
        public bool IsMain { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is IClothesImageBase clothesImage && Equals(clothesImage);

        public bool Equals(IClothesImageBase? other) =>
            other?.Id == Id;

        public override int GetHashCode() => HashCode.Combine(Id);
        #endregion
    }
}