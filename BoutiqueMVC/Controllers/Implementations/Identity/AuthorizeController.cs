﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;
using BoutiqueDAL.Models.Implementations.Identity;
using BoutiqueDTO.Models.Implementations.Identity;
using BoutiqueMVC.Models.Implementations.Identity;
using BoutiqueMVC.Models.Interfaces.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Sync;
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
        public AuthorizeController(IUserManagerBoutique userManager, ISignInManagerBoutique signInManager, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings;
          
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
        /// Авторизоваться через JWT токен
        /// </summary>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<string>> AuthorizeJwt(AuthorizeTransfer login) =>
            await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false).
            MapBindAsync(result => GetAuthorizeAction(result, login.Email));

        /// <summary>
        /// Сгенерировать токен или вернуть отказ авторизации
        /// </summary>
        private async Task<ActionResult<string>> GetAuthorizeAction(SignInResult signInResult, string userName) =>
            signInResult switch
            {
                _ when signInResult.Succeeded => await GetJwtResult(userName),
                _ when signInResult.IsLockedOut => Unauthorized(),
                _ => Unauthorized()
            };

        /// <summary>
        /// Получить ответ сервера с токеном
        /// </summary>
        private async Task<ActionResult<string>> GetJwtResult(string userName) =>
            await GetIdentityByUserName(userName).
            MapBindAsync(GenerateJwtToken);

        /// <summary>
        /// Найти пользователя по имени
        /// </summary>
        private async Task<BoutiqueUser> GetIdentityByUserName(string userName) =>
            await _userManager.Users.FirstOrDefaultAsync(r => r.UserName == userName);

        /// <summary>
        /// Сгенерировать токен
        /// </summary>
        private async Task<string> GenerateJwtToken(BoutiqueUser user) =>
             new JwtSecurityToken(_jwtSettings.Issuer, _jwtSettings.Audience,
                                  GetClaims(user, await _userManager.GetRolesAsync(user)),
                                  expires: DateTime.Now.AddDays(_jwtSettings.Expires),
                                  signingCredentials: GetCredentials(_jwtSettings)).
             Map(jwtToken => new JwtSecurityTokenHandler().WriteToken(jwtToken));

        /// <summary>
        /// Получить права доступа
        /// </summary>
        private static IEnumerable<Claim> GetClaims(IdentityUser user, IEnumerable<string> roles) =>
            new List<Claim>
            {
                new (JwtRegisteredClaimNames.Sub, user.UserName),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new (ClaimTypes.NameIdentifier, user.Id),
            }.Concat(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        /// <summary>
        /// Получить ключ для доступа
        /// </summary>
        private static SigningCredentials GetCredentials(JwtSettings jwtSettings) =>
            new SymmetricSecurityKey(jwtSettings.Key).
            Map(key => new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
    }
}