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
        byte[] Image { get; }

        /// <summary>
        /// Главное изображение
        /// </summary>
        bool IsMain { get; }
         
        /// <summary>
        /// Идентификатор одежды
        /// </summary>
        int ClothesId { get; }
    }
}