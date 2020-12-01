using System.ComponentModel.DataAnnotations;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Clothes.ClothesTypeTransfers
{
    /// <summary>
    /// Вид одежды. Базовая трансферная модель
    /// </summary>
    public interface IClothesTypeShortTransfer : IClothesType, ITransferModel<string>
    {
        /// <summary>
        /// Категория
        /// </summary>
        string CategoryName { get; set; }
    }
}