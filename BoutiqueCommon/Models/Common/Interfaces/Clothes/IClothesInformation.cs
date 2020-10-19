using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes
{
    /// <summary>
    /// Одежда. Информация
    /// </summary>
    public interface IClothesInformation: IClothesShort
    {
        /// <summary>
        /// Описание
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Пол
        /// </summary>
        GenderType GenderType { get; }

        /// <summary>
        /// Вид одежды
        /// </summary>
        string ClothesType { get; }

        /// <summary>
        /// Размеры
        /// </summary>
        IReadOnlyCollection<int> Sizes { get; }
    }
}