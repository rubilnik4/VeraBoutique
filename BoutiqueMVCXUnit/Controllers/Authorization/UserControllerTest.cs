using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueCommon.Models.Enums.Identities;
using BoutiqueCommonXUnit.Data.Authorize;
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
using ResultFunctional.Models.Implementations.Results;
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
            var users = IdentityData.BoutiqueUsers;
            var userManager = GetUserManager(users);
            var userController = new UserController(userManager.Object, RegisterTransferConverterMock.RegisterTransferConverter, 
                                                    BoutiqueUserTransferConverterMock.BoutiqueUserTransferConverter);

            var usersResult = await userController.GetRoleUsers();
            var usersZip = usersResult.Value.Zip(users);

            Assert.True(usersZip.All(user => user.Second.Email == user.First.Email));
        }

        /// <summary>
        /// Зарегистрировать
        /// </summary>
        [Fact]
        public async Task Register_Ok()
        {
            var user = IdentityEntitiesData.BoutiqueRoleUsers.First();
            var userResult = user.ToResultValue();
            var userManager = GetUserManager(userResult);
            var registerController = new UserController(userManager.Object, RegisterTransferConverterMock.RegisterTransferConverter,
                                                        BoutiqueUserTransferConverterMock.BoutiqueUserTransferConverter);
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
            var user = IdentityEntitiesData.BoutiqueRoleUsers.First();
            var userResult = ErrorResultFactory.ValueNotValidError(user, GetType(), "ValueNotValid").ToResultValue<BoutiqueRoleUser>();
            var userManager = GetUserManager(userResult);
            var registerController = new UserController(userManager.Object, RegisterTransferConverterMock.RegisterTransferConverter,
                                                        BoutiqueUserTransferConverterMock.BoutiqueUserTransferConverter);
            var register = RegisterTransferData.RegisterTransfers.First();

            var actionResult = await registerController.CreateRoleUser(register);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(CommonErrorType.ValueNotValid.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        [Fact]
        public async Task DeleteRoleUser()
        {
            var user = IdentityEntitiesData.BoutiqueRoleUsers.First();
            var userResult = user.ToResultValue();
            var userManager = GetUserManager(userResult);
            var userController = new UserController(userManager.Object, RegisterTransferConverterMock.RegisterTransferConverter,
                                                        BoutiqueUserTransferConverterMock.BoutiqueUserTransferConverter);

            var actionResult = await userController.DeleteRoleUser(user.BoutiqueUserEntity.Email);

            Assert.Equal(user.BoutiqueUserEntity.Email, actionResult.Value);
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        [Fact]
        public async Task DeleteRoleUser_NotFound()
        {
            var user = IdentityEntitiesData.BoutiqueRoleUsers.First();
            var userResult = ErrorResultFactory.ValueNotFoundError(user, GetType()).ToResultValue<BoutiqueRoleUser>();
            var userManager = GetUserManager(userResult);
            var userController = new UserController(userManager.Object, RegisterTransferConverterMock.RegisterTransferConverter,
                                                    BoutiqueUserTransferConverterMock.BoutiqueUserTransferConverter);

            var actionResult = await userController.DeleteRoleUser(user.BoutiqueUserEntity.Email);

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
            var user = IdentityEntitiesData.BoutiqueRoleUsers.First();
            var userResult = ErrorResultFactory.ValueNotValidError(user, GetType(), "ValueNotValid").ToResultValue<BoutiqueRoleUser>();
            var userManager = GetUserManager(userResult);
            var userController = new UserController(userManager.Object, RegisterTransferConverterMock.RegisterTransferConverter,
                                                        BoutiqueUserTransferConverterMock.BoutiqueUserTransferConverter);

            var actionResult = await userController.DeleteRoleUser(user.BoutiqueUserEntity.Email);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(CommonErrorType.ValueNotValid.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Удалить пользователей
        /// </summary>
        [Fact]
        public async Task DeleteRoleUsers()
        {
            var deleteResult = new ResultError();
            var users = IdentityData.BoutiqueUsers;
            var userManager = GetUserManager(deleteResult, users);
            var userController = new UserController(userManager.Object, RegisterTransferConverterMock.RegisterTransferConverter,
                                                    BoutiqueUserTransferConverterMock.BoutiqueUserTransferConverter);

            var actionResult = await userController.DeleteRoleUsers();

            Assert.IsType<NoContentResult>(actionResult);
            var noContentResult = (NoContentResult)actionResult;
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }

        /// <summary>
        /// Удалить пользователей
        /// </summary>
        [Fact]
        public async Task DeleteRoleUsers_Error()
        {
            var initialError = ErrorResultFactory.DatabaseAccessError("TestTable", "TestTable");
            var deleteResult = initialError.ToResult();
            var users = IdentityData.BoutiqueUsers;
            var userManager = GetUserManager(deleteResult, users);
            var userController = new UserController(userManager.Object, RegisterTransferConverterMock.RegisterTransferConverter,
                                                    BoutiqueUserTransferConverterMock.BoutiqueUserTransferConverter);

            var actionResult = await userController.DeleteRoleUsers();


            Assert.IsType<BadRequestObjectResult>(actionResult);
            var badRequest = (BadRequestObjectResult)actionResult;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.Id, errors.Keys.First());
        }


        /// <summary>
        /// Менеджер авторизации
        /// </summary>
        private static Mock<IUserManagerService> GetUserManager(IReadOnlyCollection<IBoutiqueUserDomain> users) =>
            new Mock<IUserManagerService>().
            Void(userMock => userMock.Setup(userManager => userManager.GetRoleUsers()).
                                      ReturnsAsync(users));

        /// <summary>
        /// Менеджер авторизации
        /// </summary>
        private static Mock<IUserManagerService> GetUserManager(IResultValue<BoutiqueRoleUser> userResult) =>
            new Mock<IUserManagerService>().
            Void(userMock => userMock.Setup(userManager => userManager.CreateRoleUser(It.IsAny<IRegisterRoleDomain>())).
                                          ReturnsAsync(userResult.ResultValueOk(user => user.BoutiqueUserEntity.Email))).
            Void(userMock => userMock.Setup(userManager => userManager.DeleteRoleUser(It.IsAny<string>())).
                                      ReturnsAsync(userResult.ResultValueOk(user => user.BoutiqueUserEntity.Email)));

        /// <summary>
        /// Менеджер авторизации
        /// </summary>
        private static Mock<IUserManagerService> GetUserManager(IResultError deleteResult, IReadOnlyCollection<IBoutiqueUserDomain> users) =>
            new Mock<IUserManagerService>().
            Void(userMock => userMock.Setup(userManager => userManager.GetUsersByRole(It.IsAny<IdentityRoleType>())).
                                            ReturnsAsync(users)).
            Void(userMock => userMock.Setup(userManager => userManager.DeleteRoleUsersByRole(It.IsAny<IdentityRoleType>())).
                                      ReturnsAsync(deleteResult));
    }
}