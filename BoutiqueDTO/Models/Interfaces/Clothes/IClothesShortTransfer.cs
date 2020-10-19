using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Clothes
{
    /// <summary>
    /// Одежда. Трансферная модель
    /// </summary>
    public interface IClothesShortTransfer : IClothesShort, ITransferModel<int>
    { }
}