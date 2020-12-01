using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain
{
    /// <summary>
    /// Группа размеров одежды разного типа
    /// </summary>
    public interface ISizeGroupDomain: ISizeGroupShortDomain, IEquatable<ISizeGroupDomain>
    {
        /// <summary>
        /// Дополнительные размеры одежды
        /// </summary>
        IReadOnlyCollection<ISizeDomain> Sizes { get; }

        /// <summary>
        /// Получить имя группы размеров по базовому типу
        /// </summary>
        string GetBaseGroupName(SizeType sizeType);
    }
}