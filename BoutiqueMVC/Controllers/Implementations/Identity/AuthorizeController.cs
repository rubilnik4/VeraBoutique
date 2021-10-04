﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Identity;
using BoutiqueDAL.Models.Implementations.Identity;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Identity;
using BoutiqueDTO.Models.Implementations.Identity;
using BoutiqueMVC.Extensions.Controllers.Sync;
using BoutiqueMVC.Models.Implementations.Identity;
using BoutiqueMVC.Models.Interfaces.Identity;
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
        public AuthorizeController(IUserManagerBoutique userManager, ISignInManagerBoutique signInManager, JwtSettings jwtSettings,
                                   IAuthorizeTransferConverter authorizeTransferConverter)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings;
            _authorizeTransferConverter = authorizeTransferConverter;
        }

        /// <summary>
        /// Менеджер авторизации
        /// </summary>
        private readonly IUserManagerBoutique _userManager;

        /// <summary>
        /// Менеджер аутентификации
        /// </summary>
        private readonly ISignInManagerBoutique _signInManager;

        /// <summary>
        /// Параметры JWT токена
        /// </summary>
        private readonly JwtSettings _jwtSettings;

        /// <summary>
        /// Конвертер логина и пароля в трансферную модель
        /// </summary>
        private readonly IAuthorizeTransferConverter _authorizeTransferConverter;

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
            await _userManager.FindByEmail(email).
            ToResultValueNullCheckTaskAsync(ErrorResultFactory.ValueNotFoundError(email, GetType())).
            WhereContinueBindAsync(result => result.OkStatus, 
                                   result => GenerateJwtToken(result.Value),
                                   result => result.Errors.GetBadRequestByErrors<string>().
                                             MapAsync(Task.FromResult));

        /// <summary>
        /// Сгенерировать токен
        /// </summary>
        private async Task<ActionResult<string>> GenerateJwtToken(BoutiqueIdentityUser identityUser) =>
             new JwtSecurityToken(_jwtSettings.Issuer, _jwtSettings.Audience,
                                  GetClaims(identityUser, await _userManager.GetRoles(identityUser)),
                                  expires: DateTime.Now.AddDays(_jwtSettings.Expires),
                                  signingCredentials: GetCredentials(_jwtSettings)).
             Map(jwtToken => new JwtSecurityTokenHandler().WriteToken(jwtToken));

        /// <summary>
        /// Получить права доступа
        /// </summary>
        private static IEnumerable<Claim> GetClaims(BoutiqueIdentityUser identityUser, IEnumerable<string> roles) =>
            new List<Claim>
            {
                new (JwtRegisteredClaimNames.Sub, identityUser.UserName),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new (ClaimTypes.NameIdentifier, identityUser.Id),
            }.Concat(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        /// <summary>
        /// Получить ключ для доступа
        /// </summary>
        private static SigningCredentials GetCredentials(JwtSettings jwtSettings) =>
            new SymmetricSecurityKey(jwtSettings.Key).
            Map(key => new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
    }
}