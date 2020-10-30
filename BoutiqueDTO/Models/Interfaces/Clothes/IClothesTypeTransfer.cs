using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Clothes
{
    /// <summary>
    /// Вид одежды. Трансферная модель
    /// </summary>
    public interface IClothesTypeTransfer : IClothesTypeShortTransfer
    {
        /// <summary>
        /// Тип пола. Трансферная модель
        /// </summary>
        GenderTransfer GenderTransfer { get; }

        /// <summary>
        /// Категория одежды. Трансферная модель
        /// </summary>
        CategoryTransfer CategoryTransfer { get; }
    }
}