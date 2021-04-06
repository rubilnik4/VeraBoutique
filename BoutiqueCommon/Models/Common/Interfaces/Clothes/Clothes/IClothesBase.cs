using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes
{
    /// <summary>
    /// Одежда
    /// </summary>
    public interface IClothesBase : IModel<int>, IEquatable<IClothesBase>
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
        /// Тип пола одежды
        /// </summary>
        GenderType GenderType { get; }

        /// <summary>
        /// Тип одежды
        /// </summary>
        string ClothesTypeName { get; }
    }
}