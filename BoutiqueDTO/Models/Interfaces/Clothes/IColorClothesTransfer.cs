using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Clothes
{
    /// <summary>
    /// Цвет одежды. Трансферная модель
    /// </summary>
    public interface IColorClothesTransfer : IColorClothes, ITransferModel<string>
    { }
}