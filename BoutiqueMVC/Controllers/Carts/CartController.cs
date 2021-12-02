using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueDTO.Models.Implementations.Carts;
using BoutiqueDTO.Models.Implementations.Identities;
using BoutiqueMVC.Infrastructure.Interfaces.Carts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        /// <summary>
        /// Сервис корзины
        /// </summary>
        private readonly ICartService _cartService;

        /// <summary>
        /// Получить пользователей с ролями
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CartMainTransfer>> CreateCart() =>
            await _cartService.CreateCart().
            MapTaskAsync(users => _boutiqueUserTransferConverter.ToTransfers(users)).
            MapTaskAsync(transfers => new ActionResult<IReadOnlyCollection<BoutiqueUserTransfer>>(transfers));
    }
}