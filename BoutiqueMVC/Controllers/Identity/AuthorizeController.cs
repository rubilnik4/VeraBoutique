using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Identities;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Identity;
using BoutiqueDTO.Models.Implementations.Identities;
using BoutiqueMVC.Extensions.Controllers.Async;
using BoutiqueMVC.Extensions.Controllers.Sync;
using BoutiqueMVC.Infrastructure.Interfaces.Identities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace BoutiqueMVC.Controllers.Identity
{
    /// <summary>
    /// Контроллер авторизации
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        public AuthorizeController(IUserManagerService userManager, ISignInManagerBoutique signInManager,
                                   IJwtTokenService jwtTokenService, IAuthorizeTransferConverter authorizeTransferConverter)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
            _authorizeTransferConverter = authorizeTransferConverter;
        }

        /// <summary>
        /// Менеджер авторизации
        /// </summary>
        private readonly IUserManagerService _userManager;

        /// <summary>
        /// Менеджер аутентификации
        /// </summary>
        private readonly ISignInManagerBoutique _signInManager;

        /// <summary>
        /// Сервис управления токенами
        /// </summary>
        private readonly IJwtTokenService _jwtTokenService;

        /// <summary>
        /// Конвертер логина и пароля в трансферную модель
        /// </summary>
        private readonly IAuthorizeTransferConverter _authorizeTransferConverter;

        /// <summary>
        /// Проверка токена
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public bool IsTokenValid() =>
            true;

        /// <summary>
        /// Авторизоваться через JWT токен
        /// </summary>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult<string>> AuthorizeJwt(AuthorizeTransfer login) =>
            await _authorizeTransferConverter.FromTransfer(login).
            ResultValueOkAsync(loginDomain => _signInManager.PasswordSignInAsync(loginDomain.Email, loginDomain.Password, false, false)).
            WhereContinueBindAsync(result => result.OkStatus,
                                   result => GetAuthorizeAction(result.Value, login.Email),
                                   result => result.Errors.GetBadRequestByErrors<string>().
                                             MapAsync(Task.FromResult));

        /// <summary>
        /// Сгенерировать токен или вернуть отказ авторизации
        /// </summary>
        private async Task<ActionResult<string>> GetAuthorizeAction(SignInResult signInResult, string email) =>
            signInResult switch
            {
                { Succeeded: true } => await GetJwtResult(email),
                { IsLockedOut: true } => Unauthorized(),
                _ => Unauthorized()
            };

        /// <summary>
        /// Получить ответ сервера с токеном
        /// </summary>
        private async Task<ActionResult<string>> GetJwtResult(string email) =>
            await _userManager.FindRoleUserByEmail(email).
            ResultValueOkTaskAsync(_jwtTokenService.GenerateJwtToken).
            ToActionResultValueTaskAsync();

        /// <summary>
        /// Получить адрес почты из токена
        /// </summary>
        public static string? GetEmail(ClaimsPrincipal user) =>
            user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}