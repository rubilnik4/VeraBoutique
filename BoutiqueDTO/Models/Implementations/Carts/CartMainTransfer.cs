using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Carts;
using BoutiqueCommon.Models.Common.Interfaces.Carts;
using BoutiqueCommon.Models.Domain.Interfaces.Carts;
using BoutiqueDTO.Models.Interfaces.Carts;
using Newtonsoft.Json;

namespace BoutiqueDTO.Models.Implementations.Carts
{
    /// <summary>
    /// Корзина. Трансферная модель
    /// </summary>
    public class CartMainTransfer : CartMainBase<CartItemTransfer>, ICartMainTransfer
    {
        public CartMainTransfer(ICartBase cart, IEnumerable<CartItemTransfer> cartItems)
           : this(cart.Id, cart.CreationDate, cart.AuthorId, cartItems.ToList())
        { }

        [JsonConstructor]
        public CartMainTransfer(Guid id, DateTime creationDate, string authorId, IReadOnlyCollection<CartItemTransfer> cartItems)
           : base(id, creationDate, authorId, cartItems)
        { }
    }
}