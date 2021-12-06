﻿using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Carts;
using BoutiqueCommon.Models.Common.Interfaces.Carts;
using BoutiqueDAL.Models.Interfaces.Entities.Carts;

namespace BoutiqueDAL.Models.Implementations.Entities.Carts
{
    /// <summary>
    /// Корзина. Сущность базы данных
    /// </summary>
    public class CartEntity : CartBase, ICartEntity
    {
        public CartEntity(ICartBase cart)
            : this(cart, null)
        { }

        public CartEntity(ICartBase cart, IEnumerable<CartItemEntity>? cartItems)
           : this(cart.Id, cartItems)
        { }

        public CartEntity(Guid id)
          : this(id, null)
        { }

        public CartEntity(Guid id, IEnumerable<CartItemEntity>? cartItems)
            : base(id)
        {
            CartItems = cartItems?.ToList();
        }

        /// <summary>
        /// Изображение
        /// </summary>
        public IReadOnlyCollection<CartItemEntity>? CartItems { get; }
    }
}