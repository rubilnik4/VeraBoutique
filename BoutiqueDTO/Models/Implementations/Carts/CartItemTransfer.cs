using BoutiqueCommon.Models.Common.Implementations.Carts;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Carts;
using BoutiqueDTO.Models.Interfaces.Carts;
using BoutiqueDTO.Models.Interfaces.Clothes;
using Newtonsoft.Json;

namespace BoutiqueDTO.Models.Implementations.Carts
{
    /// <summary>
    /// Позиция в корзине. Трансферная модель
    /// </summary>
    public class CartItemTransfer : CartItemBase, ICartItemTransfer
    {
        public CartItemTransfer(ICartItemBase cartItem)
         : this(cartItem.Id, cartItem.Name, cartItem.Price, cartItem.CartId)
        { }

        [JsonConstructor]
        public CartItemTransfer(string id, string name, decimal price, string cartId)
            : base(id, name, price, cartId)
        { }
    }
}