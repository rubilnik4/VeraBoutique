using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfer;

namespace BoutiqueDTO.Models.Interfaces.Clothes.ClothesTransfer
{
    /// <summary>
    /// Одежда. Информация. Доменная модель
    /// </summary>
    public interface IClothesFullTransfer: IClothes, IClothesShortTransfer
    {
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