using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Carts;

namespace BoutiqueCommon.Models.Common.Implementations.Carts
{
    /// <summary>
    /// Корзина
    /// </summary>
    public abstract class CartBase: ICartBase
    {
        protected CartBase(string id)
        {
            Id = id;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id { get; }

        #region IEquatable
        public override bool Equals(object? obj) =>
            obj is ICartBase cart && Equals(cart);

        public bool Equals(ICartBase? other) =>
            other?.Id == Id;

        public override int GetHashCode() =>
            HashCode.Combine(Id);
        #endregion
    }
}