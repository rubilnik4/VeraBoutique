using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Implementations.Identities;
using BoutiqueCommon.Models.Enums.Identities;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Identities;
using BoutiqueDAL.Models.Enums.Identity;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Identity;
using BoutiqueDTO.Models.Implementations.Identities;
using BoutiqueMVC.Extensions.Controllers.Async;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;

namespace BoutiqueMVC.Controllers.Identity
{
    /// <summary>
    /// Контроллер пользователей
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = IdentityPolicyType.ADMIN_POLICY)]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController(IUserManagerService userManager, IRegisterTransferConverter registerTransferConverter,
                              IBoutiqueUserTransferConverter boutiqueUserTransferConverter)
        {
            _userManager = userManager;
            _registerTransferConverter = registerTransferConverter;
            _boutiqueUserTransferConverter = boutiqueUserTransferConverter;
        }

        /// <summary>
        /// Менеджер авторизации
        /// </summary>
        private readonly IUserManagerService _userManager;

        /// <summary>
        /// Конвертер регистрации в трансферную модель
        /// </summary>
        private readonly IRegisterTransferConverter _registerTransferConverter;

        /// <summary>
        /// Конвертер пользователей в трансферную модель
        /// </summary>
        private readonly IBoutiqueUserTransferConverter _boutiqueUserTransferConverter;

        /// <summary>
        /// Получить пользователей с ролями
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<BoutiqueUserTransfer>>> GetRoleUsers() =>
            await _userManager.GetRoleUsers().
            MapTaskAsync(users => _boutiqueUserTransferConverter.ToTransfers(users)).
            MapTaskAsync(transfers => new ActionResult<IReadOnlyCollection<BoutiqueUserTransfer>>(transfers));

        /// <summary>
        /// Зарегистрироваться
        /// </summary>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult<string>> CreateRoleUser(RegisterTransfer registerTransfer) =>
            await _registerTransferConverter.FromTransfer(registerTransfer).
            ResultValueOk(register => new RegisterRoleDomain(register, IdentityRoleType.User)).
            ResultValueBindOkAsync(registerRole => _userManager.CreateRoleUser(registerRole)).
            ToActionResultValueTaskAsync();

        /// <summary>
        /// Обновить пользователя
        /// </summary>
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRoleUser(BoutiqueUserTransfer userTransfer) =>
            await _boutiqueUserTransferConverter.FromTransfer(userTransfer).
            ResultValueBindErrorsOkAsync(userRole => _userManager.UpdateRoleUser(userRole)).
            ToResultErrorTaskAsync().
            ToNoContentActionResultTaskAsync();

        /// <summary>
        /// Удалить пользователей
        /// </summary>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRoleUsers() =>
            await _userManager.DeleteRoleUsersByRole(IdentityRoleType.User).
            ToNoContentActionResultTaskAsync();

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        [HttpDelete("{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> DeleteRoleUser(string email) =>
            await _userManager.DeleteRoleUser(email).
            ToActionResultValueTaskAsync();
    }
}