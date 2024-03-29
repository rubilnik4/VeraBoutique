﻿using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Carts;

namespace BoutiqueCommon.Models.Common.Implementations.Carts
{
    public abstract class CartMainBase<TCartItem> : CartBase, ICartMainBase<TCartItem>
        where TCartItem : ICartItemBase
    {
        protected CartMainBase(Guid id, DateTime creationDate, string authorId, IEnumerable<TCartItem> cartItems)
            :base(id, creationDate, authorId)
        {
            CartItems = cartItems.ToList();
        }

        /// <summary>
        /// Позиции в корзине
        /// </summary>
        public IReadOnlyCollection<TCartItem> CartItems { get; }
    }
}