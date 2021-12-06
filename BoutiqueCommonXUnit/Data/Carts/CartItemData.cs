using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Implementations.Carts;
using BoutiqueCommon.Models.Domain.Interfaces.Carts;

namespace BoutiqueCommonXUnit.Data.Carts
{
    /// <summary>
    /// Позиции в корзине
    /// </summary>
    public static class CartItemData
    {
        /// <summary>
        /// Позиции в корзине
        /// </summary>
        public static IReadOnlyCollection<ICartItemDomain> CartItems =>
            new List<ICartItemDomain>
            {
                new CartItemDomain(Guid.NewGuid(), "Товар1", 1000, Guid.NewGuid()),
                new CartItemDomain(Guid.NewGuid(), "Товар2", 2000, Guid.NewGuid()),
            };
    }
}