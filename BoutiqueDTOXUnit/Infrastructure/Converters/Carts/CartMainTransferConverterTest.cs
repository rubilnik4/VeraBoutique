using System.Linq;
using BoutiqueCommonXUnit.Data.Carts;
using BoutiqueDTO.Models.Implementations.Carts;
using BoutiqueDTOXUnit.Data.Transfers.Carts;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes;
using ResultFunctional.Models.Interfaces.Errors.CommonErrors;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Carts
{
    /// <summary>
    /// Конвертер корзины в трансферную модель. Тесты
    /// </summary>
    public class CartMainTransferConverterTest
    {
        /// <summary>
        /// Конвертер корзины в трансферную модель. Тесты
        /// </summary>
        [Fact]
        public void ToTransfer_FromTransfer()
        {
            var cart = CartData.CartMainDomains.First();
            var cartTransferConverter = CartTransferConverterMock.CartMainTransferConverter;

            var cartTransfer = cartTransferConverter.ToTransfer(cart);
            var cartAfterConverter = cartTransferConverter.FromTransfer(cartTransfer);

            Assert.True(cartAfterConverter.OkStatus);
            Assert.True(cart.Equals(cartAfterConverter.Value));
        }

        /// <summary>
        /// Преобразования корзины в трансферную модель. Ошибка позиций
        /// </summary>
        [Fact]
        public void Cart_ToTransfer_CartItemsNull()
        {
            var cart = CartTransferData.CartMainTransfers.First();
            var cartNull = new CartMainTransfer(cart, cart.CartItems.Append(null!)!);
            var cartMainTransferConverter = CartTransferConverterMock.CartMainTransferConverter;

            var cartAfterConverter = cartMainTransferConverter.FromTransfer(cartNull);

            Assert.True(cartAfterConverter.HasErrors);
            Assert.IsAssignableFrom<IValueNotFoundErrorResult>(cartAfterConverter.Errors.First());
        }
    }
}