using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BoutiqueDTO.Models.Implementations.Identity;
using BoutiqueMVC.Models.Implementations.Identity;
using Functional.FunctionalExtensions.Async;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace BoutiqueMVC.Controllers.Implementations.Authorization
{
    /// <summary>
    /// Контроллер авторизации
    /// </summary>
    [ApiController]
    [Route("/api/auth")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
                              JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings;
        }
        /// <summary>
        /// Менеджер авторизации
        /// </summary>
        private readonly UserManager<IdentityUser> _userManager;

        /// <summary>
        /// Менеджер аутентификации
        /// </summary>
        private readonly SignInManager<IdentityUser> _signInManager;

        /// <summary>
        /// Параметры JWT токена
        /// </summary>
        private readonly JwtSettings _jwtSettings;

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> Login(IdentityLoginTransfer login) =>
            await _signInManager.PasswordSignInAsync(login.UserName, login.UserName, false, false).
            MapBindAsync(result => GetLoginAction(result, login.UserName));

        /// <summary>
        /// Сгенерировать токен или вернуть отказ авторизации
        /// </summary>
        private async Task<ActionResult<string>> GetLoginAction(SignInResult signInResult, string userName) =>
            signInResult switch
            {
                _ when signInResult.Succeeded => await GetJwtResult(userName),
                _ when signInResult.IsLockedOut => BadRequest("This account is locked out"),
                _ => BadRequest("Username or Password is incorrect")
            };

        /// <summary>
        /// Найти пользователя по имени
        /// </summary>
        private async Task<IdentityUser> GetIdentityByUserName(string userName) =>
            await _userManager.Users.SingleOrDefaultAsync(r => r.UserName == userName);

        /// <summary>
        /// Получить ответ сервера с токеном
        /// </summary>
        private async Task<ActionResult<string>> GetJwtResult(string userName) =>
            await GetIdentityByUserName(userName).
            MapBindAsync(GenerateJwtToken);

        /// <summary>
        /// Сгенерировать токен
        /// </summary>
        private async Task<string> GenerateJwtToken(IdentityUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(_jwtSettings.Key);
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims,
                expires: DateTime.Now.AddDays(_jwtSettings.Expires),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}