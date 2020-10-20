using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Base;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes
{
    /// <summary>
    /// Одежда. Информация. Трансферная модель
    /// </summary>
    public interface IClothesInformationDomain : IClothesShortDomain, IClothesInformation
    {
        /// <summary>
        /// Цвета одежды
        /// </summary>
        IReadOnlyCollection<string> Colors { get; }

        /// <summary>
        /// Размеры
        /// </summary>
        IReadOnlyCollection<int> Sizes { get; }
    }
}