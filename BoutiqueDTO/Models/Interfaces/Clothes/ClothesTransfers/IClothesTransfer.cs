using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers;

namespace BoutiqueDTO.Models.Interfaces.Clothes.ClothesTransfers
{
    /// <summary>
    /// Одежда. Доменная модель
    /// </summary>
    public interface IClothesTransfer: IClothesShortTransfer
    {
        /// <summary>
        /// Пол одежды
        /// </summary>
        GenderTransfer Gender { get; }

        /// <summary>
        /// Вид одежды
        /// </summary>
        ClothesTypeShortTransfer ClothesTypeShort { get; }

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