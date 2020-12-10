using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups
{
    /// <summary>
    /// Группа размеров одежды. Базовые данные
    /// </summary>
    public interface ISizeGroupBase<TSize> : ISizeGroupShortBase, IEquatable<ISizeGroupBase<TSize>>
        where TSize: ISizeBase
    {
        /// <summary>
        /// Размеры
        /// </summary>
        IReadOnlyCollection<TSize> Sizes { get; }

        /// <summary>
        /// Получить имя группы размеров по базовому типу
        /// </summary>
        string GetBaseGroupName(SizeType sizeType);
    }
}
