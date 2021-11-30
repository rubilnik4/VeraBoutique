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
          : base(cart.Id, cartItems)
        { }

        public CartMainDomain(string id, IEnumerable<ICartItemDomain> cartItems)
            : base(id, cartItems)
        { }
    }
}