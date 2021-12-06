using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Carts;
using BoutiqueDAL.Models.Implementations.Entities.Carts;

namespace BoutiqueDALXUnit.Data.Entities.Carts
{
    /// <summary>
    /// Данные сущностей корзин
    /// </summary>
    public static class CartEntitiesData
    {
        /// <summary>
        /// Сущности категорий одежды
        /// </summary>
        public static IReadOnlyCollection<CartEntity> CartEntities =>
            CartData.CartMainDomains.
            Select(cart => new CartEntity(cart, cart.CartItems.Select(cartItem => new CartItemEntity(cartItem)))).
            ToList();
    }
}