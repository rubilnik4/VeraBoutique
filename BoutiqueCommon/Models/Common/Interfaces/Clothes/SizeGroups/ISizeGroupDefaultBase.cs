using System;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups
{
    /// <summary>
    /// Группа размеров одежды с размером по умолчанию. Базовые данные
    /// </summary>
    public interface ISizeGroupDefaultBase<TSize> : ISizeGroupMainBase<TSize>, IFormattable
        where TSize : ISizeBase
    {
        /// <summary>
        /// Тип размера по умолчанию
        /// </summary>
        SizeType DefaultSizeType { get; }

        /// <summary>
        /// Наименование по умолчанию
        /// </summary>
        string DefaultName { get; }
    }
}