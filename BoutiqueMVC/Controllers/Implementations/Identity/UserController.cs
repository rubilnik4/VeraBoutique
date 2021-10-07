using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Identities;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Identities;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Identity;
using BoutiqueDTO.Models.Implementations.Identities;
using BoutiqueMVC.Extensions.Controllers.Async;
using BoutiqueMVC.Infrastructure.Implementation.Validation;
using BoutiqueMVC.Models.Implementations.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;

namespace BoutiqueMVC.Controllers.Implementations.Identity
{
    /// <summary>
    /// Контроллер пользователей
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        public UserController(IUserManagerService userManager, AuthorizeSettings authorizeSettings,
                              IRegisterTransferConverter registerTransferConverter)
        {
            _userManager = userManager;
            _authorizeSettings = authorizeSettings;
            _registerTransferConverter = registerTransferConverter;
        }

        /// <summary>
        /// Менеджер авторизации
        /// </summary>
        private readonly IUserManagerService _userManager;

        /// <summary>
        /// Параметры авторизации
        /// </summary>
        private readonly AuthorizeSettings _authorizeSettings;

        /// <summary>
        /// Конвертер регистрации в трансферную модель
        /// </summary>
        private readonly IRegisterTransferConverter _registerTransferConverter;

        /// <summary>
        /// Получить пользователей с ролями
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<BoutiqueUserTransfer>>> GetRoleUsers() =>
            await _userManager.GetRoleUsers().
            MapTaskAsync(users => users.Select(user => user.ToBoutiqueUser()).
                                        Select(user => new BoutiqueUserTransfer(user)).
                                        ToList());

        /// <summary>
        /// Зарегистрироваться
        /// </summary>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> CreateRoleUser(RegisterTransfer register) =>
            await _registerTransferConverter.FromTransfer(register).
            ResultValueBindOk(registerDomain => RegisterValidation.RegisterValidate(registerDomain, _authorizeSettings)).
            ResultValueBindOkAsync(registerDomain => _userManager.CreateRoleUser(registerDomain, IdentityRoleType.User)).
            ToActionResultValueTaskAsync();

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> DeleteRoleUser(string email) =>
            await  _userManager.DeleteRoleUser(email).
            ToActionResultValueTaskAsync();
    }
}