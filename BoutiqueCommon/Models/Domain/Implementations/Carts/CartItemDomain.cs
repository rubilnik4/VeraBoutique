using BoutiqueCommon.Models.Common.Implementations.Carts;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Carts;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Carts;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Carts
{
    public class CartItemDomain : CartItemBase, ICartItemDomain
    {
        public CartItemDomain(ICartItemBase cartItem)
          : this(cartItem.Name, cartItem.Price)
        { }

        public CartItemDomain(string name, decimal price)
            : base(name, price)
        { }
    }
}