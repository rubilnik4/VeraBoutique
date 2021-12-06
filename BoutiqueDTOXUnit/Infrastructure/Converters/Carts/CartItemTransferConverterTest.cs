using System.Linq;
using BoutiqueCommonXUnit.Data.Carts;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Carts
{
    /// <summary>
    /// Конвертер позиции корзины в трансферную модель. Тесты
    /// </summary>
    public class CartItemTransferConverterTest
    {
        /// <summary>
        /// Преобразования позиции корзины одежды в трансферную модель
        /// </summary>
        [Fact]
        public void ToTransfer_FromTransfer()
        {
            var cartItem = CartItemData.CartItems.First();
            var cartItemTransferConverter = CartItemTransferConverterMock.CartItemTransferConverter;

            var cartItemTransfer = cartItemTransferConverter.ToTransfer(cartItem);
            var cartItemAfterConverter = cartItemTransferConverter.FromTransfer(cartItemTransfer);

            Assert.True(cartItemAfterConverter.OkStatus);
            Assert.True(cartItem.Equals(cartItemAfterConverter.Value));
        }
    }
}