using System;
using BoutiqueCommon.Models.Common.Interfaces.Base;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes.Images
{
    /// <summary>
    /// Изображение. Трансферная модель
    /// </summary>
    public interface IClothesImageBase : IModel<int>, IEquatable<IClothesImageBase>
    {
        /// <summary>
        /// Изображение
        /// </summary>
        public byte[] Image { get; }

        /// <summary>
        /// Главное изображение
        /// </summary>
        public bool IsMain { get; }
    }
}