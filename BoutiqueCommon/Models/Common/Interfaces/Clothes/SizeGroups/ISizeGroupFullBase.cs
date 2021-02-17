using System;
using System.Collections.Generic;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups
{
    /// <summary>
    /// Группа размеров одежды с включенными размерами
    /// </summary>
    public interface ISizeGroupFullBase<TSize>: ISizeGroupBase, IEquatable<ISizeGroupFullBase<TSize>>
    {
        /// <summary>
        /// Размеры одежды
        /// </summary>
        IReadOnlyCollection<TSize> Sizes { get; }
    }
}