﻿using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Carts;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Images;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueCommonXUnit.Data.Carts;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes;
using BoutiqueMVC.Controllers.Carts;
using BoutiqueMVC.Controllers.Clothes;
using BoutiqueMVC.Infrastructure.Interfaces.Carts;
using BoutiqueMVCXUnit.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;
using Xunit;

namespace BoutiqueMVCXUnit.Controllers.Carts
{
    /// <summary>
    /// Корзина. Тесты
    /// </summary>
    public class CartControllerTest
    {
        /// <summary>
        /// Получить корзину. Корректный вариант
        /// </summary>
        [Fact]
        public async Task CreateCart_Ok()
        {
            var user = IdentityData.BoutiqueUsers.First();
            var cartDomain = CartData.CartDomains.First();
            var cartResult = new ResultValue<ICartDomain>(cartDomain);
            var cartService = GetCartService(cartResult);
            var cartTransferConverter = CartTransferConverterMock.CartTransferConverter;
            var claimUser = ClaimsData.GetClaimsIdentity(user.Email);
            var cartController = GetCartController(cartService.Object, claimUser);

            var cartTransfer = await cartController.CreateCart();
            var actionResult = cartTransferConverter.FromTransfer(cartTransfer.Value);

            Assert.True(actionResult.Value.Equals(cartDomain));
        }

        /// <summary>
        /// Получить корзину. Ошибка базы данных
        /// </summary>
        [Fact]
        public async Task CreateCart_ErrorDatabase()
        {
            var user = IdentityData.BoutiqueUsers.First();
            var initialError = ErrorData.ErrorTest;
            var cartResult = new ResultValue<ICartDomain>(initialError);
            var cartService = GetCartService(cartResult);
            var claimUser = ClaimsData.GetClaimsIdentity(user.Email);
            var cartController = GetCartController(cartService.Object, claimUser);

            var actionResult = await cartController.CreateCart();

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.Id, errors.Keys.First());
        }

        /// <summary>
        /// Получить корзину. Ошибка почты
        /// </summary>
        [Fact]
        public async Task CreateCart_ErrorEmail()
        {
            var cartDomain = CartData.CartDomains.First();
            var cartResult = new ResultValue<ICartDomain>(cartDomain);
            var cartService = GetCartService(cartResult);
            var cartController = GetCartController(cartService.Object, null!);

            var actionResult = await cartController.CreateCart();

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }

        /// <summary>
        /// Сервис одежды в базе данных
        /// </summary>
        private static Mock<ICartService> GetCartService(IResultValue<ICartDomain> cartResult) =>
            new Mock<ICartService>().
            Void(mock => mock.Setup(service => service.CreateCart(It.IsAny<string>())).
                              ReturnsAsync(cartResult));

        /// <summary>
        /// Получить контроллер корзины
        /// </summary>
        private static CartController GetCartController(ICartService cartService, ClaimsPrincipal claimUser) =>
            new(cartService, CartTransferConverterMock.CartTransferConverter)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = claimUser } }
            };
    }
}