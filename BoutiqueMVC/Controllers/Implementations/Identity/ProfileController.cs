using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Identities;
using BoutiqueDAL.Models.Enums.Identity;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Identity;
using BoutiqueDTO.Models.Implementations.Identities;
using BoutiqueDTO.Routes.Clothes;
using BoutiqueMVC.Extensions.Controllers.Async;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Errors;

namespace BoutiqueMVC.Controllers.Implementations.Identity
{
    /// <summary>
    /// Личные данные. Контроллер
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        public ProfileController(IUserManagerService userManager, IBoutiqueUserTransferConverter boutiqueUserTransferConverter)
        {
            _userManager = userManager;
            _boutiqueUserTransferConverter = boutiqueUserTransferConverter;
        }

        /// <summary>
        /// Менеджер пользователей
        /// </summary>
        private readonly IUserManagerService _userManager;

        /// <summary>
        /// Конвертер пользователей в трансферную модель
        /// </summary>
        private readonly IBoutiqueUserTransferConverter _boutiqueUserTransferConverter;

        /// <summary>
        /// Получить личные данные пользователя
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BoutiqueUserTransfer>> GetProfile() =>
            await AuthorizeController.GetEmail(User).
            ToResultValueNullCheck(ErrorResultFactory.ValueNotFoundError(ClaimTypes.NameIdentifier, GetType())).
            ResultValueBindOkAsync(email => _userManager.FindRoleUserByEmail(email)).
            ResultValueOkTaskAsync(user => _boutiqueUserTransferConverter.ToTransfer(user)).
            ToActionResultValueTaskAsync();
    }
}