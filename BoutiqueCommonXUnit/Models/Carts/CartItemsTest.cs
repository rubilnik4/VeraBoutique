using System;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Carts;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.CategoryDomains;
using BoutiqueCommonXUnit.Data.Carts;
using BoutiqueCommonXUnit.Data.Clothes;
using Xunit;

namespace BoutiqueCommonXUnit.Models.Carts
{
    /// <summary>
    /// Позиции в корзине. Тесты
    /// </summary>
    public class CartItemsTest
    {
        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void CartItem_Equal_Ok()
        {
            var id = Guid.NewGuid();
            const string name = "name";
            const decimal price = 1000;
            var cartId = Guid.NewGuid();
            var cartItemDomain = new CartItemDomain(id, name, price, cartId);

            int cartItemHash = HashCode.Combine(id);
            Assert.Equal(cartItemHash, cartItemDomain.GetHashCode());
        }

        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void CartItem_Equal_CartItem()
        {
            var first = CartItemData.CartItems.First();
            var second = new CartItemDomain(first);

            Assert.True(first.Equals(second));
        }
    }
}