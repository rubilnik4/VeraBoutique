using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Carts;
using BoutiqueDTO.Models.Implementations.Carts;
using BoutiqueDTO.Models.Implementations.Identities;
using BoutiqueMVC.Extensions.Controllers.Async;
using BoutiqueMVC.Infrastructure.Implementation.Identities;
using BoutiqueMVC.Infrastructure.Interfaces.Carts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;

namespace BoutiqueMVC.Controllers.Carts
{
    /// <summary>
    /// Контроллер корзины
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class CartController: ControllerBase
    {
        public CartController(ICartService cartService, ICartTransferConverter cartTransferConverter)
        {
            _cartService = cartService;
            _cartTransferConverter = cartTransferConverter;
        }

        /// <summary>
        /// Сервис корзины
        /// </summary>
        private readonly ICartService _cartService;

        /// <summary>
        /// Конвертер корзины в трансферную модель
        /// </summary>
        private readonly ICartTransferConverter _cartTransferConverter;

        /// <summary>
        /// Получить пользователей с ролями
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CartTransfer>> CreateCart() =>
            await ClaimsInformation.GetEmail(User).
            ResultValueBindOkAsync(email => _cartService.CreateCart(email)).
            ResultValueOkTaskAsync(cart => _cartTransferConverter.ToTransfer(cart)).
            ToActionResultValueTaskAsync<Guid, CartTransfer>();
    }
}