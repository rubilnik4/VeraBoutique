using System;
using BoutiqueCommon.Models.Common.Implementations.Carts;
using BoutiqueCommon.Models.Common.Interfaces.Carts;
using BoutiqueDAL.Models.Interfaces.Entities.Carts;

namespace BoutiqueDAL.Models.Implementations.Entities.Carts
{
    /// <summary>
    /// Позиция в корзине. Сущность базы данных
    /// </summary>
    public class CartItemEntity : CartItemBase, ICartItemEntity
    {
        public CartItemEntity(ICartItemBase cartItem)
            : this(cartItem.Id, cartItem.Name, cartItem.Price, cartItem.CartId)
        { }

        public CartItemEntity(Guid id, string name, decimal price, Guid cartId)
            : this(id, name, price, cartId, null)
        { }

        public CartItemEntity(ICartItemBase cartItem, CartEntity cartEntity)
            : this(cartItem.Id, cartItem.Name, cartItem.Price, cartItem.CartId, cartEntity)
        { }

        public CartItemEntity(Guid id, string name, decimal price, Guid cartId, CartEntity? cartEntity)
          : base(id, name, price, cartId)
        {
            CartEntity = cartEntity;
        }

        /// <summary>
        /// Корзина
        /// </summary>
        public CartEntity? CartEntity { get; }
    }
}