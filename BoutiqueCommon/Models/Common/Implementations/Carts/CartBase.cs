using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Carts;

namespace BoutiqueCommon.Models.Common.Implementations.Carts
{
    /// <summary>
    /// Корзина
    /// </summary>
    public abstract class CartBase<TCartItem> : ICartBase<TCartItem>
        where TCartItem : ICartItemBase
    {
        protected CartBase(IEnumerable<TCartItem> cartItems)
        {
            CartItems = cartItems.ToList();
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public abstract string Id { get; }

        /// <summary>
        /// Позиции в корзине
        /// </summary>
        public IReadOnlyCollection<TCartItem> CartItems { get; }

        /// <summary>
        /// Сумма
        /// </summary>
        public decimal Total =>
            CartItems.
            Sum(cartItem => cartItem.Price);
    }
}