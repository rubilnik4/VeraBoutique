using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;

namespace BoutiqueDTO.Models.Interfaces.Clothes
{
    /// <summary>
    /// Одежда. Информация. Доменная модель
    /// </summary>
    public interface IClothesInformationTransfer: IClothesInformation, IClothesShortTransfer
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