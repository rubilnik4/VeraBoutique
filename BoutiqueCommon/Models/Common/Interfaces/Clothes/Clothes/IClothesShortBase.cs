using System;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes
{
    /// <summary>
    /// Одежда
    /// </summary>
    public interface IClothesShortBase : IModel<int>, IEquatable<IClothesShortBase>
    {
        /// <summary>
        /// Наименование
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Описание
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Цена
        /// </summary>
        decimal Price { get; }

        /// <summary>
        /// Изображение
        /// </summary>
        byte[] Image { get; }

        /// <summary>
        /// Тип пола одежды
        /// </summary>
        GenderType GenderType { get; }

        /// <summary>
        /// Тип одежды
        /// </summary>
        string ClothesTypeName { get; }
    }
}