using BoutiqueDTO.Infrastructure.Implementations.Converters.Carts;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Carts;

namespace BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes
{
    /// <summary>
    /// Конвертер корзины в трансферную модель
    /// </summary>
    public static class CartTransferConverterMock
    {
        /// <summary>
        /// Конвертер корзины в трансферную модель
        /// </summary>
        public static ICartTransferConverter CartTransferConverter =>
            new CartTransferConverter();

        /// <summary>
        /// Конвертер корзины в трансферную модель
        /// </summary>
        public static ICartMainTransferConverter CartMainTransferConverter =>
            new CartMainTransferConverter(CartItemTransferConverterMock.CartItemTransferConverter);
    }
}