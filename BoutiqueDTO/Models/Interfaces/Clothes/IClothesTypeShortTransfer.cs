using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Clothes
{
    /// <summary>
    /// Вид одежды. Основная информация. Трансферная модель
    /// </summary>
    public interface IClothesTypeShortTransfer : IClothesType, ITransferModel<string>
    { }
}