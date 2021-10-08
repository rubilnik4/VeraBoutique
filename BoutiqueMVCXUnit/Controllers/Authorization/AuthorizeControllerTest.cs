using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Identities;
using BoutiqueDAL.Models.Enums.Identity;
using BoutiqueDAL.Models.Implementations.Identities;
using BoutiqueDALXUnit.Data.Identity;
using BoutiqueDTOXUnit.Data.Transfers.Authorize;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Identity;
using BoutiqueMVC.Controllers.Implementations.Identity;
using BoutiqueMVC.Infrastructure.Interfaces.Identities;
using BoutiqueMVC.Models.Implementations.Identity;
using BoutiqueMVCXUnit.Data.Controllers.Implementations;
using Microsoft.AspNetCore.Http;
using ResultFunctional.FunctionalExtensions.Sync;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MockQueryable.Moq;
using Moq;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Interfaces.Results;
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
            var user = IdentityData.BoutiqueUsers.First();
            var userResult = user.ToResultValue();
            var userManager = GetUserManager(userResult);
            var signInManager = GetSignInManager(SignInSuccess);
            var loginController = new AuthorizeController(userManager.Object, signInManager.Object, JwtSettings, 
                                                          AuthorizeTransferConverterMock.AuthorizeTransferConverter);

            var tokenResult = await loginController.AuthorizeJwt(AuthorizeTransfersData.AuthorizeTransfers.First());
            var handler = new JwtSecurityTokenHandler();
            var tokenDecode = handler.ReadToken(tokenResult.Value) as JwtSecurityToken;
            var claims = tokenDecode?.Claims.ToList();
            var claimRole = claims?.First(claim => claim.Type == ClaimTypes.Role && claim.Value == user.IdentityRoleType.ToString());

            Assert.True(!String.IsNullOrWhiteSpace(tokenResult.Value));
            Assert.NotNull(claimRole);
        }

        /// <summary>
        /// Сгенерировать токен
        /// </summary>
        [Fact]
        public async Task Login_UserNotFound()
        {
            var user = IdentityData.BoutiqueUsers.First();
            var userResult = ErrorResultFactory.ValueNotFoundError(user, GetType()).ToResultValue<IBoutiqueUserDomain>();
            var userManager = GetUserManager(userResult);
            var signInManager = GetSignInManager(SignInSuccess);
            var loginController = new AuthorizeController(userManager.Object, signInManager.Object, JwtSettings,
                                                          AuthorizeTransferConverterMock.AuthorizeTransferConverter);

            var actionResult = await loginController.AuthorizeJwt(AuthorizeTransfersData.AuthorizeTransfers.First());

            Assert.IsType<NotFoundResult>(actionResult.Result);
            var notFoundResult = (NotFoundResult)actionResult.Result;
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        /// <summary>
        /// Ошибка. Аккаунт блокирован
        /// </summary>
        [Fact]
        public async Task Login_LockOut()
        {
            var user = IdentityData.BoutiqueUsers.First(); 
            var userResult = user.ToResultValue();
            var userManager = GetUserManager(userResult);
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
            var user = IdentityData.BoutiqueUsers.First();
            var userResult = user.ToResultValue();
            var userManager = GetUserManager(userResult);
            var signInManager = GetSignInManager(SignInIncorrectLogin);
            var loginController = new AuthorizeController(userManager.Object, signInManager.Object, JwtSettings,
                                                          AuthorizeTransferConverterMock.AuthorizeTransferConverter);

            var tokenResult = await loginController.AuthorizeJwt(AuthorizeTransfersData.AuthorizeTransfers.First());
            Assert.IsType<UnauthorizedResult>(tokenResult.Result);
        }

        /// <summary>
        /// Менеджер авторизации
        /// </summary>
        private static Mock<IUserManagerService> GetUserManager(IResultValue<IBoutiqueUserDomain> userResult) =>
            new Mock<IUserManagerService>().
            Void(userMock => userMock.Setup(userManager => userManager.FindRoleUserByEmail(It.IsAny<string>())).
                                      ReturnsAsync(userResult));

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