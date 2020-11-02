using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Clothes.ClothesTypeTransfer
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