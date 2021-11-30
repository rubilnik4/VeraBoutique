using System.Linq;
using BoutiqueDTO.Models.Implementations.Carts;
using BoutiqueDTO.Models.Implementations.Clothes.CategoryTransfers;
using BoutiqueDTOXUnit.Data.Transfers.Carts;
using BoutiqueDTOXUnit.Data.Transfers.Clothes;
using Newtonsoft.Json;
using Xunit;

namespace BoutiqueDTOXUnit.Json.Carts
{
    /// <summary>
    /// Позиция в корзине
    /// </summary>
    public class CartItemToJsonTest
    {
        /// <summary>
        /// Преобразовать в Json
        /// </summary>
        [Fact]
        public void ToJson_Ok()
        {
            var cartItemTransfer = CartItemTransferData.CartItemTransfers.First();

            string json = JsonConvert.SerializeObject(cartItemTransfer);
            var cartItemAfterJson = JsonConvert.DeserializeObject<CartItemTransfer>(json);

            Assert.True(cartItemAfterJson?.Equals(cartItemTransfer));
        }
    }
}