using BoutiqueDTO.Models.Implementations.Clothes;

namespace BoutiqueDTO.Models.Interfaces.Clothes.ClothesTypeTransfers
{
    /// <summary>
    /// Вид одежды. Основная информация. Трансферная модель
    /// </summary>
    public interface IClothesTypeShortTransfer : IClothesTypeTransfer
    {
        /// <summary>
        /// Тип пола. Трансферная модель
        /// </summary>
        GenderTransfer Gender { get; }
    }
}