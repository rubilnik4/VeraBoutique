using BoutiqueCommon.Models.Common.Implementations.Carts;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Carts;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Carts;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Carts
{
    /// <summary>
    /// Позиция в корзине. Доменная модель
    /// </summary>
    public class CartItemDomain : CartItemBase, ICartItemDomain
    {
        public CartItemDomain(ICartItemBase cartItem)
            : this(cartItem.Id, cartItem.Name, cartItem.Price, cartItem.CartId)
        { }

        public CartItemDomain(string id, string name, decimal price, string cartId) 
            : base(id, name, price, cartId)
        { }
    }
}