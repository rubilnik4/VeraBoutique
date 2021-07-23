using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups
{
    /// <summary>
    /// Группа размеров одежды с включенными размерами
    /// </summary>
    public interface ISizeGroupMainBase<TSize>: ISizeGroupBase, IEquatable<ISizeGroupMainBase<TSize>>
        where TSize : ISizeBase
    {
        /// <summary>
        /// Размеры одежды
        /// </summary>
        IReadOnlyCollection<TSize> Sizes { get; }

        /// <summary>
        /// Получить имя группы размеров по базовому типу
        /// </summary>
        string GetBaseGroupName(SizeType sizeType);
    }
}