using System;
using BoutiqueCommon.Models.Common.Interfaces.Base;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes
{
    /// <summary>
    /// Одежда
    /// </summary>
    public interface IClothesMain : IModel<int>, IEquatable<IClothesMain>
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
        byte[]? Image { get; }
    }
}