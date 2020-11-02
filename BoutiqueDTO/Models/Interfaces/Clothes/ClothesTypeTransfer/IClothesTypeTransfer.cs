using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Clothes.ClothesTypeTransfer
{
    /// <summary>
    /// Вид одежды. Трансферная модель
    /// </summary>
    public interface IClothesTypeTransfer : IClothesType, ITransferModel<string>
    {
        /// <summary>
        /// Категория одежды. Трансферная модель
        /// </summary>
        CategoryTransfer Category { get; }
    }
}