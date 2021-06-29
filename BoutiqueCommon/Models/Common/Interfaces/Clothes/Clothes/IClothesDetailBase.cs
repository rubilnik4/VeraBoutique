using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes
{
    /// <summary>
    /// Одежда. Уточненная информация
    /// </summary>
    public interface IClothesDetailBase<TColor, TSizeGroup, TSize> :
        IClothesBase,
        IEquatable<IClothesDetailBase<TColor, TSizeGroup, TSize>>
        where TColor : IColorBase
        where TSizeGroup : ISizeGroupMainBase<TSize>
        where TSize : ISizeBase
    {
        /// <summary>
        /// Цвета одежды
        /// </summary>
        IReadOnlyCollection<TColor> Colors { get; }

        /// <summary>
        /// Размеры
        /// </summary>
        IReadOnlyCollection<TSizeGroup> SizeGroups { get; }
    }
}