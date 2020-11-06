using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Clothes.ClothesTransfers
{
    /// <summary>
    /// Одежда. Базовая трансферная модель
    /// </summary>
    public interface IClothesShortTransfer : IClothesShort, ITransferModel<int>
    { }
}