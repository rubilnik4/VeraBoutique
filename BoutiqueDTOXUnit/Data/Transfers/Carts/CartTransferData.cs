using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Carts;
using BoutiqueDTO.Models.Implementations.Carts;

namespace BoutiqueDTOXUnit.Data.Transfers.Carts
{
    /// <summary>
    /// Корзина. Трансферные модели
    /// </summary>
    public static class CartTransferData
    {
        /// <summary>
        /// Позиции в корзине. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<CartMainTransfer> CartMainTransfers =>
            CartData.CartMainDomains.
            Select(cart => new CartMainTransfer(cart, cart.CartItems.Select(cartItem => new CartItemTransfer(cartItem)).ToList())).
            ToList();

        /// <summary>
        /// Позиции в корзине. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<CartTransfer> CartTransfers =>
            CartData.CartDomains.
            Select(cart => new CartTransfer(cart)).
            ToList();
    }
}