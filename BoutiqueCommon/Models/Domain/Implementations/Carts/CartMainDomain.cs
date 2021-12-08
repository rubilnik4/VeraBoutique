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
    public class CartMainDomain : CartMainBase<ICartItemDomain>, ICartMainDomain
    {
        public CartMainDomain(ICartBase cart, IEnumerable<ICartItemDomain> cartItems)
          : base(cart.Id, cart.CreationDate, cart.AuthorId, cartItems)
        { }

        public CartMainDomain(Guid id, DateTime creationDate, string authorId, IEnumerable<ICartItemDomain> cartItems)
            : base(id, creationDate, authorId, cartItems)
        { }
    }
}