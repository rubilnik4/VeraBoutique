using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Carts;

namespace BoutiqueCommon.Models.Common.Implementations.Carts
{
    public abstract class CartMainBase<TCartItem> : CartBase, ICartMainBase<TCartItem>
        where TCartItem : ICartItemBase
    {
        protected CartMainBase(string id, IEnumerable<TCartItem> cartItems)
            :base(id)
        {
            CartItems = cartItems.ToList();
        }

        /// <summary>
        /// Позиции в корзине
        /// </summary>
        public IReadOnlyCollection<TCartItem> CartItems { get; }
    }
}