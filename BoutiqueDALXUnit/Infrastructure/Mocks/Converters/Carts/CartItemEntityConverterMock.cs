using BoutiqueDAL.Infrastructure.Implementations.Converters.Carts;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.CategoryEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Carts;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.CategoryEntities;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Converters.Carts
{
    /// <summary>
    /// Преобразования модели позиции корзины в модель базы данных
    /// </summary>
    public static class CartItemEntityConverterMock
    {
        /// <summary>
        /// Преобразования модели позиции корзины в модель базы данных
        /// </summary>
        public static ICartItemEntityConverter CartItemEntityConverter =>
            new CartItemEntityConverter();
    }
}