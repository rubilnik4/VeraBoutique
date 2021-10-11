using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Identities;
using BoutiqueDAL.Models.Enums.Identity;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Identity;
using BoutiqueDTO.Models.Implementations.Identities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResultFunctional.FunctionalExtensions.Async;

namespace BoutiqueMVC.Controllers.Implementations.Identity
{
    /// <summary>
    /// Контроллер ролей
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = IdentityPolicyType.ADMIN_POLICY)]
    [ApiController]
    public class RoleController : ControllerBase
    {
        public RoleController(IRoleStoreService roleStore)
        {
            _roleStore = roleStore;
        }

        /// <summary>
        /// Управление ролями
        /// </summary>
        private readonly IRoleStoreService _roleStore;

        /// <summary>
        /// Получить пользователей с ролями
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<string>>> GetRoles() =>
            await _roleStore.GetRoles().
            MapTaskAsync(roles => new ActionResult<IReadOnlyCollection<string>>(roles));
    }
}