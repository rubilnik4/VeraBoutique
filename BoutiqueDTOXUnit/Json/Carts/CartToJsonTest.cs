using System.Linq;
using BoutiqueDTO.Models.Implementations.Carts;
using BoutiqueDTOXUnit.Data.Transfers.Carts;
using Newtonsoft.Json;
using Xunit;

namespace BoutiqueDTOXUnit.Json.Carts
{
    /// <summary>
    /// Корзина
    /// </summary>
    public class CartToJsonTest
    {
        /// <summary>
        /// Преобразовать в Json
        /// </summary>
        [Fact]
        public void ToJson_Ok()
        {
            var cartTransfer = CartTransferData.CartTransfers.First();

            string json = JsonConvert.SerializeObject(cartTransfer);
            var cartAfterJson = JsonConvert.DeserializeObject<CartTransfer>(json);

            Assert.True(cartAfterJson?.Equals(cartTransfer));
        }

        /// <summary>
        /// Преобразовать в Json
        /// </summary>
        [Fact]
        public void ToJsonMain_Ok()
        {
            var cartTransfer = CartTransferData.CartMainTransfers.First();

            string json = JsonConvert.SerializeObject(cartTransfer);
            var cartAfterJson = JsonConvert.DeserializeObject<CartTransfer>(json);

            Assert.True(cartAfterJson?.Equals(cartTransfer));
        }
    }
}