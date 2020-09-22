using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BoutiqueDTO.Models.Implementations.Identity;
using BoutiqueMVC.Controllers.Implementations.Authorization;
using BoutiqueMVC.Models.Implementations.Identity;
using BoutiqueMVC.Models.Interfaces.Identity;
using BoutiqueMVCXUnit.Data.Controllers.Implementations;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace BoutiqueMVCXUnit.Controllers.Authorization
{
    /// <summary>
    /// Контроллер авторизации. Тесты
    /// </summary>
    public class LoginControllerTest
    {
        [Fact]
        public async Task Login_GenerateToken()
        {
            var userManager = GetUserManager();
            var signInManager = GetSignInManager(SignInSuccess);
            var jwtSettings = JwtSettings;
            var loginController = new LoginController(userManager.Object, signInManager.Object, jwtSettings);

            var tokenResult = await loginController.Login(LoginData.IdentityLogin);

            Assert.True(!String.IsNullOrWhiteSpace(tokenResult.Value));
        }

        /// <summary>
        /// Менеджер авторизации
        /// </summary>
        private static Mock<IUserManagerBoutique> GetUserManager() =>
            new Mock<IUserManagerBoutique>().
            Void(userMock => userMock.Setup(userManager => userManager.Users).Returns(LoginData.Users.AsQueryable()));

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
        /// Параметры JWT токена
        /// </summary>
        private static JwtSettings JwtSettings =>
            new byte[100].
            Void(key => RandomNumberGenerator.Create().GetBytes(key)).
            Map(key => new JwtSettings("Test", "Test", 1, key));
    }
}