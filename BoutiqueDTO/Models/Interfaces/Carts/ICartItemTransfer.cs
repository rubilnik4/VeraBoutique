using BoutiqueCommon.Models.Common.Interfaces.Carts;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Carts
{
    /// <summary>
    /// Позиция в корзине. Трансферная модель
    /// </summary>
    public interface ICartItemTransfer: ICartItemBase, ITransferModel<string>
    { }
}