using BoutiqueDTO.Infrastructure.Implementations.Converters.Carts;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Carts;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;

namespace BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes
{
    /// <summary>
    /// Конвертер позиции корзины в трансферную модель
    /// </summary>
    public static class CartItemTransferConverterMock
    {
        /// <summary>
        /// Конвертер позиции корзины в трансферную модель
        /// </summary>
        public static ICartItemTransferConverter CartItemTransferConverter =>
            new CartItemTransferConverter();
    }
}