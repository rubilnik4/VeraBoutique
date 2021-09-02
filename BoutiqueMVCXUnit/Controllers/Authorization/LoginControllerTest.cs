using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BoutiqueMVC.Controllers.Implementations.Authorize;
using BoutiqueMVC.Models.Implementations.Identity;
using BoutiqueMVC.Models.Interfaces.Identity;
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
    public class LoginControllerTest
    {
        /// <summary>
        /// Сгенерировать токен
        /// </summary>
        [Fact]
        public async Task Login_GenerateToken()
        {
            var userManager = GetUserManager();
            var signInManager = GetSignInManager(SignInSuccess);
            var jwtSettings = JwtSettings;
            var loginController = new AuthorizeController(userManager.Object, signInManager.Object, jwtSettings);

            var tokenResult = await loginController.AuthorizeJwt(LoginData.Authorize);
            var handler = new JwtSecurityTokenHandler();
            var tokenDecode = handler.ReadToken(tokenResult.Value) as JwtSecurityToken;
            var claims = tokenDecode?.Claims.ToList();
            var claimRole = claims?.First(claim => claim.Type == ClaimTypes.Role && claim.Value == LoginData.Roles.First());

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
            var jwtSettings = JwtSettings;
            var loginController = new AuthorizeController(userManager.Object, signInManager.Object, jwtSettings);

            var tokenResult = await loginController.AuthorizeJwt(LoginData.Authorize);
            Assert.IsType<BadRequestObjectResult>(tokenResult.Result);
        }

        /// <summary>
        /// Ошибка. Неправильное имя пользователя и пароль
        /// </summary>
        [Fact]
        public async Task Login_IncorrectLogin()
        {
            var userManager = GetUserManager();
            var signInManager = GetSignInManager(SignInIncorrectLogin);
            var jwtSettings = JwtSettings;
            var loginController = new AuthorizeController(userManager.Object, signInManager.Object, jwtSettings);

            var tokenResult = await loginController.AuthorizeJwt(LoginData.Authorize);
            Assert.IsType<BadRequestObjectResult>(tokenResult.Result);
        }

        /// <summary>
        /// Менеджер авторизации
        /// </summary>
        private static Mock<IUserManagerBoutique> GetUserManager() =>
            new Mock<IUserManagerBoutique>().
            Void(userMock => userMock.Setup(userManager => userManager.Users).
                                      Returns(LoginData.Users.AsQueryable().BuildMock().Object)).
            Void(userMock => userMock.Setup(userManager => userManager.GetRolesAsync(It.IsAny<IdentityUser>())).
                                      ReturnsAsync(LoginData.Roles));

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