using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Clothes
{
    /// <summary>
    /// Вид одежды. Трансферная модель
    /// </summary>
    public interface IClothesTypeTransfer : IClothesTypeBase, ITransferModel<string>
    { }
}