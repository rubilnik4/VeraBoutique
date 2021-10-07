using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueCommon.Models.Enums.Identities;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Identities;
using BoutiqueDAL.Models.Implementations.Identities;
using BoutiqueDALXUnit.Data.Identity;
using BoutiqueDTO.Models.Implementations.Identities;
using BoutiqueDTOXUnit.Data.Transfers.Authorize;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Identity;
using BoutiqueMVC.Controllers.Implementations.Identity;
using BoutiqueMVC.Models.Implementations.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Interfaces.Results;
using Xunit;

namespace BoutiqueMVCXUnit.Controllers.Authorization
{
    /// <summary>
    /// Контроллер пользователей. Тесты
    /// </summary>
    public class UserControllerTest
    {
        /// <summary>
        /// Получить пользователей с ролями
        /// </summary>
        [Fact]
        public async Task GetRoleUsers()
        {
            var users = IdentityData.BoutiqueRoleUsers;
            var userManager = GetUserManager(users);
            var userController = new UserController(userManager.Object, AuthorizeSettings, 
                                                    RegisterTransferConverterMock.RegisterTransferConverter);

            var usersResult = await userController.GetRoleUsers();

            Assert.True(usersResult.Value.Zip(users).
                                    All(user => user.Second.EqualToBoutiqueUser(user.First)));
        }

        /// <summary>
        /// Зарегистрировать
        /// </summary>
        [Fact]
        public async Task Register_Ok()
        {
            var user = IdentityData.BoutiqueRoleUsers.First();
            var userResult = user.ToResultValue();
            var userManager = GetUserManager(userResult);
            var registerController = new UserController(userManager.Object, AuthorizeSettings, RegisterTransferConverterMock.RegisterTransferConverter);
            var register = RegisterTransferData.RegisterTransfers.First();

            var actionResult = await registerController.CreateRoleUser(register);

            Assert.Equal(register.Authorize.Email, actionResult.Value);
        }

        /// <summary>
        /// Зарегистрировать
        /// </summary>
        [Fact]
        public async Task Register_RegisterError()
        {
            var user = IdentityData.BoutiqueRoleUsers.First();
            var userResult = ErrorResultFactory.ValueNotValidError(user, GetType(), "ValueNotValid").ToResultValue<BoutiqueRoleUser>();
            var userManager = GetUserManager(userResult);
            var registerController = new UserController(userManager.Object, AuthorizeSettings,
                                                            RegisterTransferConverterMock.RegisterTransferConverter);
            var register = RegisterTransferData.RegisterTransfers.First();

            var actionResult = await registerController.CreateRoleUser(register);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(CommonErrorType.ValueNotValid.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Зарегистрировать. Ошибка при проверке
        /// </summary>
        [Fact]
        public async Task Register_ValidateError()
        {
            var user = IdentityData.BoutiqueRoleUsers.First();
            var userResult = ErrorResultFactory.ValueNotFoundError(user, GetType()).ToResultValue<BoutiqueRoleUser>();
            var userManager = GetUserManager(userResult);
            var registerController = new UserController(userManager.Object, AuthorizeSettings,
                                                            RegisterTransferConverterMock.RegisterTransferConverter);
            var authorize = new AuthorizeTransfer("", "testTest07071");
            var register = new RegisterTransfer(authorize, PersonalTransferData.PersonalTransfers.First());

            var actionResult = await registerController.CreateRoleUser(register);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(AuthorizeErrorType.Email.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        [Fact]
        public async Task DeleteRoleUser()
        {
            var user = IdentityData.BoutiqueRoleUsers.First();
            var userResult = user.ToResultValue();
            var userManager = GetUserManager(userResult);
            var userController = new UserController(userManager.Object, AuthorizeSettings,
                                                    RegisterTransferConverterMock.RegisterTransferConverter);

            var actionResult = await userController.DeleteRoleUser(user.BoutiqueIdentityUser.Email);

            Assert.Equal(user.BoutiqueIdentityUser.Email, actionResult.Value);
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        [Fact]
        public async Task DeleteRoleUser_NotFound()
        {
            var user = IdentityData.BoutiqueRoleUsers.First();
            var userResult = ErrorResultFactory.ValueNotFoundError(user, GetType()).ToResultValue<BoutiqueRoleUser>();
            var userManager = GetUserManager(userResult);
            var userController = new UserController(userManager.Object, AuthorizeSettings,
                                                    RegisterTransferConverterMock.RegisterTransferConverter);

            var actionResult = await userController.DeleteRoleUser(user.BoutiqueIdentityUser.Email);

            Assert.IsType<NotFoundResult>(actionResult.Result);
            var notFoundResult = (NotFoundResult)actionResult.Result;
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        [Fact]
        public async Task DeleteRoleUser_Error()
        {
            var user = IdentityData.BoutiqueRoleUsers.First();
            var userResult = ErrorResultFactory.ValueNotValidError(user, GetType(), "ValueNotValid").ToResultValue<BoutiqueRoleUser>();
            var userManager = GetUserManager(userResult);
            var userController = new UserController(userManager.Object, AuthorizeSettings,
                                                    RegisterTransferConverterMock.RegisterTransferConverter);

            var actionResult = await userController.DeleteRoleUser(user.BoutiqueIdentityUser.Email);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(CommonErrorType.ValueNotValid.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Менеджер авторизации
        /// </summary>
        private static Mock<IUserManagerService> GetUserManager(IReadOnlyCollection<BoutiqueRoleUser> users) =>
            new Mock<IUserManagerService>().
            Void(userMock => userMock.Setup(userManager => userManager.GetRoleUsers()).
                                      ReturnsAsync(users));

        /// <summary>
        /// Менеджер авторизации
        /// </summary>
        private static Mock<IUserManagerService> GetUserManager(IResultValue<BoutiqueRoleUser> userResult) =>
            new Mock<IUserManagerService>().
            Void(userMock => userMock.Setup(userManager => userManager.CreateRoleUser(It.IsAny<IRegisterDomain>(), It.IsAny<IdentityRoleType>())).
                                          ReturnsAsync(userResult.ResultValueOk(user => user.BoutiqueIdentityUser.Email))).
            Void(userMock => userMock.Setup(userManager => userManager.DeleteRoleUser(It.IsAny<string>())).
                                      ReturnsAsync(userResult.ResultValueOk(user => user.BoutiqueIdentityUser.Email)));

        /// <summary>
        /// Параметры авторизации
        /// </summary>
        private static AuthorizeSettings AuthorizeSettings =>
            new(8, true, false, true);
    }
}