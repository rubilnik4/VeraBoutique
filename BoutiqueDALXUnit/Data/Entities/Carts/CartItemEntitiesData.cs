using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Carts;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Carts;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDALXUnit.Data.Entities.Carts
{
    /// <summary>
    /// Данные сущностей позиций корзин
    /// </summary>
    public static class CartItemEntitiesData
    {
        /// <summary>
        /// Сущности категорий одежды
        /// </summary>
        public static IReadOnlyCollection<CartItemEntity> CartItemEntities =>
            CartItemData.CartItems.
            Select(cartItem => new CartItemEntity(cartItem)).
            ToList();
    }
}