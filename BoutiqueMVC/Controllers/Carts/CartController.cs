using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueDTO.Models.Implementations.Carts;
using BoutiqueDTO.Models.Implementations.Identities;
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
        public CartController()
        {

        }

        /// <summary>
        /// Получить пользователей с ролями
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CartTransfer> CreateCart() =>
            await _userManager.GetRoleUsers().
            MapTaskAsync(users => _boutiqueUserTransferConverter.ToTransfers(users)).
            MapTaskAsync(transfers => new ActionResult<IReadOnlyCollection<BoutiqueUserTransfer>>(transfers));
    }
}