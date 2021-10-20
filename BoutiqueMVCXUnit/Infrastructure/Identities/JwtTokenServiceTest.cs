using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueDTOXUnit.Data.Transfers.Authorize;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Identity;
using BoutiqueMVC.Controllers.Implementations.Identity;
using BoutiqueMVC.Infrastructure.Implementation.Identities;
using BoutiqueMVC.Models.Implementations.Identity;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Xunit;

namespace BoutiqueMVCXUnit.Infrastructure.Identities
{
    /// <summary>
    /// Сервис управления токенами. Тесты
    /// </summary>
    public class JwtTokenServiceTest
    {
        /// <summary>
        /// Сгенерировать токен
        /// </summary>
        [Fact]
        public void Login_GenerateToken()
        {
            var user = IdentityData.BoutiqueUsers.First();
            var jwtTokenService = new JwtTokenService(JwtSettings);

            var token = jwtTokenService.GenerateJwtToken(user);
            var isValid = jwtTokenService.IsTokenValid(token);
            var handler = new JwtSecurityTokenHandler();
            var tokenDecode = handler.ReadToken(token) as JwtSecurityToken;
            var claims = tokenDecode?.Claims.ToList();
            var claimRole = claims?.First(claim => claim.Type == ClaimTypes.Role && claim.Value == user.IdentityRoleType.ToString());

            Assert.True(!String.IsNullOrWhiteSpace(token));
            Assert.True(isValid);
            Assert.NotNull(claimRole);
        }

        /// <summary>
        /// Параметры JWT токена
        /// </summary>
        private static JwtSettings JwtSettings =>
           new byte[100].
           Void(key => RandomNumberGenerator.Create().GetBytes(key)).
           Map(key => new JwtSettings("Test", "Test", 1, key));
    }
}