using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;

namespace BoutiqueDTO.Models.Interfaces.Clothes
{
    /// <summary>
    /// Одежда. Информация. Доменная модель
    /// </summary>
    public interface IClothesInformationTransfer: IClothesInformation, IClothesShortTransfer
    {
        /// <summary>
        /// Тип пола
        /// </summary>
        GenderTransfer GenderTransfer { get; }

        /// <summary>
        /// Вид одежды
        /// </summary>
        ClothesTypeTransfer ClothesTypeTransfer { get; }

        /// <summary>
        /// Цвета одежды
        /// </summary>
        IReadOnlyCollection<ColorClothesTransfer> Colors { get; }

        /// <summary>
        /// Размеры
        /// </summary>
        IReadOnlyCollection<SizeGroupTransfer> SizeGroups { get; }
    }
}