using BoutiqueDAL.Infrastructure.Implementations.Converters.Carts;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Carts;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Converters.Carts
{
    /// <summary>
    /// Преобразования модели позиции корзины в модель базы данных
    /// </summary>
    public static class CartEntityConverterMock
    {
        /// <summary>
        /// Преобразования модели позиции корзины в модель базы данных
        /// </summary>
        public static ICartMainEntityConverter CartMainEntityConverter =>
            new CartMainEntityConverter(CartItemEntityConverterMock.CartItemEntityConverter);
    }
}