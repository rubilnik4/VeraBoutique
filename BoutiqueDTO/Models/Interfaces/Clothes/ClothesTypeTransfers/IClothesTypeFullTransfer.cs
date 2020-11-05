using System.Collections.Generic;
using BoutiqueDTO.Models.Implementations.Clothes;

namespace BoutiqueDTO.Models.Interfaces.Clothes.ClothesTypeTransfers
{
    /// <summary>
    /// Вид одежды. Полная информация. Трансферная модель
    /// </summary>
    public interface IClothesTypeFullTransfer : IClothesTypeTransfer
    {
        /// <summary>
        /// Типы пола. Трансферная модель
        /// </summary>
        IReadOnlyCollection<GenderTransfer> Genders { get; }
    }
}