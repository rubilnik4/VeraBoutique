using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueDAL.Models.Implementations.Identity;
using BoutiqueDAL.Models.Interfaces.Identity;
using BoutiqueDTOXUnit.Data.Transfers.Authorize;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Identity;
using BoutiqueMVC.Controllers.Implementations.Identity;
using BoutiqueMVC.Models.Implementations.Identity;
using BoutiqueMVCXUnit.Data.Controllers.Implementations;
using ResultFunctional.FunctionalExtensions.Sync;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MockQueryable.Moq;
using Moq;
using Xunit;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace BoutiqueMVCXUnit.Controllers.Authorization
{
    /// <summary>
    /// Контроллер авторизации. Тесты
    /// </summary>
    public class AuthorizeControllerTest
    {
        /// <summary>
        /// Сгенерировать токен
        /// </summary>
        [Fact]
        public async Task Login_GenerateToken()
        {
            var userManager = GetUserManager();
            var signInManager = GetSignInManager(SignInSuccess);
            var loginController = new AuthorizeController(userManager.Object, signInManager.Object, JwtSettings, 
                                                          AuthorizeTransferConverterMock.AuthorizeTransferConverter);

            var tokenResult = await loginController.AuthorizeJwt(AuthorizeTransfersData.AuthorizeTransfers.First());
            var handler = new JwtSecurityTokenHandler();
            var tokenDecode = handler.ReadToken(tokenResult.Value) as JwtSecurityToken;
            var claims = tokenDecode?.Claims.ToList();
            var claimRole = claims?.First(claim => claim.Type == ClaimTypes.Role && claim.Value == IdentityData.Roles.First());

            Assert.True(!String.IsNullOrWhiteSpace(tokenResult.Value));
            Assert.NotNull(claimRole);
        }

        /// <summary>
        /// Ошибка. Аккаунт блокирован
        /// </summary>
        [Fact]
        public async Task Login_LockOut()
        {
            var userManager = GetUserManager();
            var signInManager = GetSignInManager(SignInLockOut);
            var loginController = new AuthorizeController(userManager.Object, signInManager.Object, JwtSettings,
                                                          AuthorizeTransferConverterMock.AuthorizeTransferConverter);

            var tokenResult = await loginController.AuthorizeJwt(AuthorizeTransfersData.AuthorizeTransfers.First());
            Assert.IsType<UnauthorizedResult>(tokenResult.Result);
        }

        /// <summary>
        /// Ошибка. Неправильное имя пользователя и пароль
        /// </summary>
        [Fact]
        public async Task Login_IncorrectLogin()
        {
            var userManager = GetUserManager();
            var signInManager = GetSignInManager(SignInIncorrectLogin);
            var loginController = new AuthorizeController(userManager.Object, signInManager.Object, JwtSettings,
                                                          AuthorizeTransferConverterMock.AuthorizeTransferConverter);

            var tokenResult = await loginController.AuthorizeJwt(AuthorizeTransfersData.AuthorizeTransfers.First());
            Assert.IsType<UnauthorizedResult>(tokenResult.Result);
        }

        /// <summary>
        /// Менеджер авторизации
        /// </summary>
        private static Mock<IUserManagerBoutique> GetUserManager() =>
            new Mock<IUserManagerBoutique>().
            Void(userMock => userMock.Setup(userManager => userManager.Users).
                                      Returns(IdentityData.Users.AsQueryable().BuildMock().Object)).
            Void(userMock => userMock.Setup(userManager => userManager.GetRolesAsync(It.IsAny<BoutiqueUser>())).
                                      ReturnsAsync(IdentityData.Roles));

        /// <summary>
        /// Менеджер аутентификации
        /// </summary>
        private static Mock<ISignInManagerBoutique> GetSignInManager(SignInResult signInResult) =>
            new Mock<ISignInManagerBoutique>().
            Void(singMock => singMock.Setup(signInManager => signInManager.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), 
                                                                                               It.IsAny<bool>(), It.IsAny<bool>())).
                                      ReturnsAsync(signInResult));

        /// <summary>
        /// Ответ сервера авторизации
        /// </summary>
        private static SignInResult SignInSuccess => SignInResult.Success;

        /// <summary>
        /// Аккаунт блокирован
        /// </summary>
        private static SignInResult SignInLockOut => SignInResult.LockedOut;

        /// <summary>
        /// Неправильное имя пользователя и пароль
        /// </summary>
        private static SignInResult SignInIncorrectLogin => SignInResult.Failed;

        /// <summary>
        /// Параметры JWT токена
        /// </summary>
        private static JwtSettings JwtSettings =>
            new byte[100].
            Void(key => RandomNumberGenerator.Create().GetBytes(key)).
            Map(key => new JwtSettings("Test", "Test", 1, key));
    }
}