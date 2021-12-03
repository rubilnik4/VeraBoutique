using BoutiqueCommon.Models.Domain.Interfaces.Carts;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Implementations.Carts;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Carts
{
    /// <summary>
    /// Конвертер корзины в трансферную модель
    /// </summary>
    public interface ICartTransferConverter : ITransferConverter<string, ICartDomain, CartTransfer>
    { }
}