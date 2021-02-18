using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Clothes.ClothesTypeTransfers
{
    /// <summary>
    /// Вид одежды. Трансферная модель
    /// </summary>
    public interface IClothesTypeTransfer : IClothesTypeBase, ITransferModel<string>
    { }
}