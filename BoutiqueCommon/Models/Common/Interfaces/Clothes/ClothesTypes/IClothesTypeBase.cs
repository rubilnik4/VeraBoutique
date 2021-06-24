using System;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes
{
    /// <summary>
    /// Вид одежды. Базовые данные
    /// </summary>
    public interface IClothesTypeBase : IModel<string>, IEquatable<IClothesTypeBase>
    {
        /// <summary>
        /// Наименование
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Тип размера по умолчанию
        /// </summary>
        SizeType SizeTypeDefault { get; }

        /// <summary>
        /// Категория
        /// </summary>
        string CategoryName { get; }
    }
}