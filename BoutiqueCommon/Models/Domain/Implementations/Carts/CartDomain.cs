using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Implementations.Carts;
using BoutiqueCommon.Models.Common.Interfaces.Carts;
using BoutiqueCommon.Models.Domain.Interfaces.Carts;

namespace BoutiqueCommon.Models.Domain.Implementations.Carts
{
    /// <summary>
    /// Корзина. Доменная модель
    /// </summary>
    public class CartDomain : CartBase, ICartDomain
    {
        public CartDomain(ICartBase cart)
            : this(cart.Id, cart.CreationDate, cart.AuthorId)
        { }

        public CartDomain(Guid id, DateTime creationDate, string authorId)
            : base(id, creationDate, authorId)
        { }
    }
}