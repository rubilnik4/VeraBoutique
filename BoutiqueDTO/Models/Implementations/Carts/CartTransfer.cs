﻿using System;
using System.Collections.Generic;
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
    public class CartTransfer : CartBase, ICartTransfer
    {
        public CartTransfer(ICartBase cart)
         : this(cart.Id, cart.CreationDate, cart.AuthorId)
        { }

        [JsonConstructor]
        public CartTransfer(Guid id, DateTime creationDate, string authorId)
           : base(id, creationDate, authorId)
        { }
    }
}