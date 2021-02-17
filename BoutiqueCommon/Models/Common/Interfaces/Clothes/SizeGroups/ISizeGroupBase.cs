using System;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups
{
    /// <summary>
    /// Группа размеров одежды. Базовые данные
    /// </summary>
    public interface ISizeGroupBase : IModel<int>, IEquatable<ISizeGroupBase>
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