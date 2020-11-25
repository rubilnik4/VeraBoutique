﻿using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes
{
    /// <summary>
    /// Группа размеров одежды разного типа
    /// </summary>
    public interface ISizeGroup: IModel<(ClothesSizeType, int)>, IEquatable<ISizeGroup>
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