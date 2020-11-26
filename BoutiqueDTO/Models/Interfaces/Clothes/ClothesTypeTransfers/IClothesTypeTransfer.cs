using System.Collections.Generic;
using BoutiqueDTO.Models.Implementations.Clothes;

namespace BoutiqueDTO.Models.Interfaces.Clothes.ClothesTypeTransfers
{
    /// <summary>
    /// Вид одежды. Трансферная модель
    /// </summary>
    public interface IClothesTypeTransfer : IClothesTypeShortTransfer
    {
        /// <summary>
        /// Категория одежды. Трансферная модель
        /// </summary>
        CategoryTransfer Category { get; }

        /// <summary>
        /// Типы пола. Трансферная модель
        /// </summary>
        IReadOnlyCollection<GenderTransfer> Genders { get; }
    }
}