using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Identities;
using BoutiqueDTOXUnit.Data.Transfers.Authorize;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Identity;
using BoutiqueMVC.Controllers.Identity;
using BoutiqueMVC.Infrastructure.Interfaces.Identities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Interfaces.Results;
using Xunit;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace BoutiqueMVCXUnit.Controllers.Identity
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
            const string token = "Token";
            var user = IdentityData.BoutiqueUsers.First();
            var userResult = user.ToResultValue();
            var userManager = GetUserManager(userResult);
            var signInManager = GetSignInManager(SignInResult.Success);
            var jwtTokenService = GetJwtTokenService(token);
            var loginController = new AuthorizeController(userManager.Object, signInManager.Object, jwtTokenService.Object, 
                                                          AuthorizeTransferConverterMock.AuthorizeTransferConverter);

            var tokenResult = await loginController.AuthorizeJwt(AuthorizeTransfersData.AuthorizeTransfers.First());

            Assert.Equal(token, tokenResult.Value);
        }

        /// <summary>
        /// Сгенерировать токен
        /// </summary>
        [Fact]
        public async Task Login_UserNotFound()
        {
            const string token = "Token";
            var user = IdentityData.BoutiqueUsers.First();
            var userResult = ErrorResultFactory.ValueNotFoundError(user, GetType()).ToResultValue<IBoutiqueUserDomain>();
            var userManager = GetUserManager(userResult);
            var signInManager = GetSignInManager(SignInResult.Success);
            var jwtTokenService = GetJwtTokenService(token);
            var loginController = new AuthorizeController(userManager.Object, signInManager.Object, jwtTokenService.Object,
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
            const string token = "Token";
            var user = IdentityData.BoutiqueUsers.First(); 
            var userResult = user.ToResultValue();
            var userManager = GetUserManager(userResult);
            var signInManager = GetSignInManager(SignInResult.LockedOut);
            var jwtTokenService = GetJwtTokenService(token);
            var loginController = new AuthorizeController(userManager.Object, signInManager.Object, jwtTokenService.Object,
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
            const string token = "Token";
            var user = IdentityData.BoutiqueUsers.First();
            var userResult = user.ToResultValue();
            var userManager = GetUserManager(userResult);
            var signInManager = GetSignInManager(SignInResult.Failed);
            var jwtTokenService = GetJwtTokenService(token);
            var loginController = new AuthorizeController(userManager.Object, signInManager.Object, jwtTokenService.Object,
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
        /// Сервис токенов
        /// </summary>
        private static Mock<IJwtTokenService> GetJwtTokenService(string token) =>
            new Mock<IJwtTokenService>().
            Void(singMock => singMock.Setup(signInManager => signInManager.GenerateJwtToken(It.IsAny<IBoutiqueUserDomain>())).
                                      Returns(token));
    }
}