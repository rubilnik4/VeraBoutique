using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Clothes.ClothesTransfers
{
    /// <summary>
    /// Одежда. Базовая трансферная модель
    /// </summary>
    public interface IClothesShortTransfer : IClothesShortBase, ITransferModel<int>
    {
        /// <summary>
        /// Тип пола одежды
        /// </summary>
        GenderType GenderType { get; }

        /// <summary>
        /// Тип одежды
        /// </summary>
        string ClothesTypeName { get; }
    }
}