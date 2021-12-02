using BoutiqueCommon.Models.Domain.Interfaces.Carts;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Implementations.Carts;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Carts
{
    /// <summary>
    /// Конвертер позиций корзины в трансферную модель
    /// </summary>
    public interface ICartItemTransferConverter : ITransferConverter<string, ICartItemDomain, CartItemTransfer>
    { }
}