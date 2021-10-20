using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Common.Interfaces.Identities;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Identities;
using BoutiqueDAL.Models.Implementations.Entities.Identities;
using BoutiqueDAL.Models.Implementations.Identities;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Identity;
using BoutiqueDTO.Models.Implementations.Identities;
using BoutiqueMVC.Extensions.Controllers.Async;
using BoutiqueMVC.Extensions.Controllers.Sync;
using BoutiqueMVC.Infrastructure.Interfaces.Identities;
using BoutiqueMVC.Models.Implementations.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace BoutiqueMVC.Controllers.Implementations.Identity
{
    /// <summary>
    /// Контроллер авторизации
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
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
        /// Сервис управления токенами
        /// </summary>
        [HttpGet("{token}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public bool IsTokenValid(string token) =>
           _jwtTokenService.IsTokenValid(token);

        /// <summary>
        /// Авторизоваться через JWT токен
        /// </summary>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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


    }
}