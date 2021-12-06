using System.Linq;
using BoutiqueCommonXUnit.Data.Carts;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Carts;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.CategoryEntities;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Converters.Carts
{
    /// <summary>
    /// Преобразования модели позиции корзины в модель базы данных. Тесты
    /// </summary>
    public class CartItemEntityConverterTest
    {
        /// <summary>
        /// Преобразования модели позиции корзины в модель базы данных. Тесты
        /// </summary>
        [Fact]
        public void ToEntity_FromEntity()
        {
            var cartItemDomain = CartItemData.CartItems.First();
            var cartItemEntityConverter = new CartItemEntityConverter();

            var cartItemEntity = cartItemEntityConverter.ToEntity(cartItemDomain);
            var cartItemAfterConverter = cartItemEntityConverter.FromEntity(cartItemEntity);

            Assert.True(cartItemAfterConverter.OkStatus);
            Assert.True(cartItemDomain.Equals(cartItemAfterConverter.Value));
        }
    }
}