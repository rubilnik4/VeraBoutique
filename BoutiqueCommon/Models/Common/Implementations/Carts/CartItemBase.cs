using System;
using BoutiqueCommon.Models.Common.Interfaces.Carts;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;

namespace BoutiqueCommon.Models.Common.Implementations.Carts
{
    /// <summary>
    /// Позиция в корзине
    /// </summary>
    public abstract class CartItemBase: ICartItemBase
    {
        protected CartItemBase(string id, string name, decimal price, string cartId)
        {
            Id = id;
            Name = name;
            Price = price;
            CartId = cartId;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; }

        /// <summary>
        /// Корзина
        /// </summary>
        public string CartId { get; }

        #region IEquatable
        public override bool Equals(object? obj) =>
            obj is ICartItemBase cartItem && Equals(cartItem);

        public bool Equals(ICartItemBase? other) =>
            other?.Id == Id;

        public override int GetHashCode() =>
            HashCode.Combine(Id);
        #endregion
    }
}