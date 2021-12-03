using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Carts;
using BoutiqueDTO.Models.Implementations.Carts;
using BoutiqueDTO.Models.Implementations.Identities;
using BoutiqueMVC.Extensions.Controllers.Async;
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
        public CartController(ICartService cartService, ICartTransferConverter cartTransferConverter,
                              ICartMainTransferConverter cartMainTransferConverter)
        {
            _cartService = cartService;
            _cartTransferConverter = cartTransferConverter;
            _cartMainTransferConverter = cartMainTransferConverter;
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
        /// Конвертер корзины в трансферную модель
        /// </summary>
        private readonly ICartMainTransferConverter _cartMainTransferConverter;

        /// <summary>
        /// Получить пользователей с ролями
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CartTransfer>> CreateCart() =>
            await _cartService.CreateCart().
            ResultValueOkTaskAsync(cart => _cartTransferConverter.ToTransfer(cart)).
            ToActionResultValueTaskAsync<string, CartTransfer>();
    }
}