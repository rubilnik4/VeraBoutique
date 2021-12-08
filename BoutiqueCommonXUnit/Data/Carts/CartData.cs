using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Carts;
using BoutiqueCommon.Models.Domain.Interfaces.Carts;

namespace BoutiqueCommonXUnit.Data.Carts
{
    /// <summary>
    /// Корзина
    /// </summary>
    public static class CartData
    {
        /// <summary>
        /// Корзина
        /// </summary>
        public static IReadOnlyCollection<ICartDomain> CartDomains =>
            CartMainDomains;

        /// <summary>
        /// Корзина
        /// </summary>
        public static IReadOnlyCollection<ICartMainDomain> CartMainDomains { get; } =
            new List<ICartMainDomain>
            {
                new CartMainDomain(Guid.NewGuid(), DateTime.Now, "rubilnik4@yandex.ru", CartItemData.CartItems.Take(1)),
                new CartMainDomain(Guid.NewGuid(), DateTime.Now, "rubilnik4@yandex.ru", CartItemData.CartItems.Skip(1)),
            };
    }
}