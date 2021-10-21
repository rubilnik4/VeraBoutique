using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueMVC.Infrastructure.Interfaces.Identities;
using BoutiqueMVC.Models.Implementations.Identity;
using Microsoft.IdentityModel.Tokens;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;

namespace BoutiqueMVC.Infrastructure.Implementation.Identities
{
    /// <summary>
    /// Сервис управления токенами
    /// </summary>
    public class JwtTokenService: IJwtTokenService
    {
        public JwtTokenService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        /// <summary>
        /// Обработчик токенов
        /// </summary>
        private readonly JwtSecurityTokenHandler _tokenHandler =
            new();

        /// <summary>
        /// Параметры JWT токена
        /// </summary>
        private readonly JwtSettings _jwtSettings;

        /// <summary>
        /// Сгенерировать токен
        /// </summary>
        public string GenerateJwtToken(IBoutiqueUserDomain user) =>
             new JwtSecurityToken(_jwtSettings.Issuer, _jwtSettings.Audience,
                                  GetClaims(user, new List<string> { user.IdentityRoleType.ToString() }),
                                  signingCredentials: GetCredentials(_jwtSettings)).
             Map(jwtToken => _tokenHandler.WriteToken(jwtToken));

        /// <summary>
        /// Получить права доступа
        /// </summary>
        private static IEnumerable<Claim> GetClaims(IBoutiqueUserDomain user, IEnumerable<string> roles) =>
            new List<Claim>
            {
                new (JwtRegisteredClaimNames.Sub, user.UserName),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new (ClaimTypes.NameIdentifier, user.Id),
            }.
            Concat(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        /// <summary>
        /// Получить ключ для доступа
        /// </summary>
        private static SigningCredentials GetCredentials(JwtSettings jwtSettings) =>
            new SymmetricSecurityKey(jwtSettings.Key).
            Map(key => new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
    }
}