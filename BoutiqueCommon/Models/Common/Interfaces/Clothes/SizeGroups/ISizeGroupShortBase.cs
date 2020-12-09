using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups
{
    /// <summary>
    /// Группа размеров одежды. Базовые данные
    /// </summary>
    public interface ISizeGroupShortBase : IModel<(ClothesSizeType, int)>
    {
        /// <summary>
        /// Тип одежды для определения размера
        /// </summary>
        ClothesSizeType ClothesSizeType { get; }

        /// <summary>
        /// Номинальное значение размера
        /// </summary>
        int SizeNormalize { get; }
    }
}