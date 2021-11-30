using System;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Carts;
using BoutiqueCommonXUnit.Data.Carts;
using Xunit;

namespace BoutiqueCommonXUnit.Models.Carts
{
    /// <summary>
    /// Корзина. Тесты
    /// </summary>
    public class CartTest
    {
        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void Cart_Equal_Ok()
        {
            const string id = "id";
            var cartDomain = new CartDomain(id);

            int cartHash = HashCode.Combine(id);
            Assert.Equal(cartHash, cartDomain.GetHashCode());
        }

        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void Cart_Equal_CartItem()
        {
            var first = CartData.CartDomains.First();
            var second = new CartDomain(first);

            Assert.True(first.Equals(second));
        }
    }
}