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
            var id = Guid.NewGuid();
            var cartDomain = new CartDomain(id, DateTime.Now, "rubilnik4@yandex.ru");

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