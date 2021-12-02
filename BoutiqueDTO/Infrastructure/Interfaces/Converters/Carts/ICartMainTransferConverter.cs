using BoutiqueCommon.Models.Domain.Interfaces.Carts;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Implementations.Carts;
using BoutiqueDTO.Models.Implementations.Clothes.CategoryTransfers;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Carts
{
    /// <summary>
    /// Конвертер корзины в трансферную модель
    /// </summary>
    public interface ICartMainTransferConverter : ITransferConverter<string, ICartMainDomain, CartMainTransfer>
    { }
}