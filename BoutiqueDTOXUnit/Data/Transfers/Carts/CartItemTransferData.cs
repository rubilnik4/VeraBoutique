using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Carts;
using BoutiqueDTO.Models.Implementations.Carts;

namespace BoutiqueDTOXUnit.Data.Transfers.Carts
{
    /// <summary>
    /// Позиции в корзине. Трансферные модели
    /// </summary>
    public class CartItemTransferData
    {
        /// <summary>
        /// Позиции в корзине. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<CartItemTransfer> CartItemTransfers =>
            CartItemData.CartItems.
            Select(cartItem => new CartItemTransfer(cartItem)).
            ToList();
    }
}