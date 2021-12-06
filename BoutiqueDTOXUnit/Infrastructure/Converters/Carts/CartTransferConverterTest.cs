using System.Linq;
using BoutiqueCommonXUnit.Data.Carts;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Carts
{
    /// <summary>
    /// Конвертер корзины в трансферную модель. Тесты
    /// </summary>
    public class CartTransferConverterTest
    {
        /// <summary>
        /// Конвертер корзины в трансферную модель. Тесты
        /// </summary>
        [Fact]
        public void ToTransfer_FromTransfer()
        {
            var cart = CartData.CartDomains.First();
            var cartTransferConverter = CartTransferConverterMock.CartTransferConverter;

            var cartTransfer = cartTransferConverter.ToTransfer(cart);
            var cartAfterConverter = cartTransferConverter.FromTransfer(cartTransfer);

            Assert.True(cartAfterConverter.OkStatus);
            Assert.True(cart.Equals(cartAfterConverter.Value));
        }
    }
}