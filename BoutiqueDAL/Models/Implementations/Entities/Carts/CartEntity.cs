using System;
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
           : this(cart.Id, cart.CreationDate, cart.AuthorId, cartItems)
        { }

        public CartEntity(Guid id, DateTime creationDate, string authorId)
          : this(id, creationDate, authorId, null)
        { }

        public CartEntity(Guid id, DateTime creationDate, string authorId, IEnumerable<CartItemEntity>? cartItems)
            : base(id, creationDate, authorId)
        {
            CartItems = cartItems?.ToList();
        }

        /// <summary>
        /// Изображение
        /// </summary>
        public IReadOnlyCollection<CartItemEntity>? CartItems { get; }
    }
}