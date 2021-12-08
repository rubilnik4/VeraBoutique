using System;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Carts;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Carts;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Carts;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes;
using BoutiqueMVC.Infrastructure.Implementation.Carts;
using BoutiqueMVC.Infrastructure.Interfaces.Carts;
using Moq;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.CommonErrors;
using ResultFunctional.Models.Interfaces.Results;
using Xunit;

namespace BoutiqueMVCXUnit.Infrastructure.Carts
{
    /// <summary>
    /// Сервис корзины. Тесты
    /// </summary>
    public class CartServiceTest
    {
        /// <summary>
        /// Получить корзину
        /// </summary>
        [Fact]
        public async Task CreateCart_Ok()
        {
            const string email = "rubilnik4@yandex.ru";
            var cartDomain = CartData.CartMainDomains.First();
            var cartResult = new ResultValue<ICartMainDomain>(cartDomain);
            var cartDatabaseService = GetCartDatabaseService(cartResult);
            var cartService = new CartService(cartDatabaseService.Object);

            var cartCreated = await cartService.CreateCart(email);
          
            Assert.True(cartCreated.Value.Equals(cartDomain));
        }

        /// <summary>
        /// Получить корзину
        /// </summary>
        [Fact]
        public async Task CreateCart_Error()
        {
            const string email = "rubilnik4@yandex.ru";
            var initialError = ErrorData.ErrorTest;
            var cartResult = new ResultValue<ICartMainDomain>(initialError);
            var cartDatabaseService = GetCartDatabaseService(cartResult);
            var cartService = new CartService(cartDatabaseService.Object);

            var cartCreated = await cartService.CreateCart(email);

            Assert.True(cartCreated.HasErrors);
            Assert.IsType(initialError.GetType(), cartCreated.Errors.First());
        }

        /// <summary>
        /// Получить корзину
        /// </summary>
        [Fact]
        public async Task CreateCart_ErrorEmail()
        {
            const string email = "rubilnik4";
            var cartDomain = CartData.CartMainDomains.First();
            var cartResult = new ResultValue<ICartMainDomain>(cartDomain);
            var cartDatabaseService = GetCartDatabaseService(cartResult);
            var cartService = new CartService(cartDatabaseService.Object);

            var cartCreated = await cartService.CreateCart(email);

            Assert.True(cartCreated.HasErrors);
            Assert.IsAssignableFrom<IValueNotValidErrorResult>(cartCreated.Errors.First());
        }

        /// <summary>
        /// Сервис корзин в базе данных
        /// </summary>
        private static Mock<ICartDatabaseService> GetCartDatabaseService(IResultValue<ICartMainDomain> cartResult) =>
            new Mock<ICartDatabaseService>().
            Void(mock => mock.Setup(service => service.Post(It.IsAny<ICartMainDomain>())).
                              ReturnsAsync(cartResult.ResultValueOk(cart => cart.Id))).
            Void(mock => mock.Setup(service => service.Get(It.IsAny<Guid>())).
                              ReturnsAsync(cartResult));
    }
}