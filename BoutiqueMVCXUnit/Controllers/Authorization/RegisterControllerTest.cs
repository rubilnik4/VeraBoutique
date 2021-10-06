using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueCommon.Models.Enums.Identities;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Identities;
using BoutiqueDAL.Models.Enums.Identity;
using BoutiqueDAL.Models.Implementations.Identities;
using BoutiqueDTO.Models.Implementations.Identities;
using BoutiqueDTOXUnit.Data.Transfers.Authorize;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Identity;
using BoutiqueMVC.Controllers.Implementations.Identity;
using BoutiqueMVC.Models.Implementations.Identity;
using BoutiqueMVCXUnit.Data.Controllers.Implementations;
using BoutiqueMVCXUnit.Data.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MockQueryable.Moq;
using Moq;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Errors.AuthorizeErrors;
using ResultFunctional.Models.Interfaces.Results;
using Xunit;

namespace BoutiqueMVCXUnit.Controllers.Authorization
{
    /// <summary>
    /// Регистрация. Тесты
    /// </summary>
    public class RegisterControllerTest
    {
        /// <summary>
        /// Зарегистрировать
        /// </summary>
        [Fact]
        public async Task Register_Ok()
        {
            var user = IdentityData.BoutiqueRoleUser;
            var userResult = user.ToResultValue();
            var userNotFound = ErrorResultFactory.ValueNotFoundError(user, GetType()).ToResultValue<BoutiqueRoleUser>();
            var userManager = GetUserManager(userResult, userNotFound);
            var registerController = new RegisterController(userManager.Object, AuthorizeSettings,
                                                            RegisterTransferConverterMock.RegisterTransferConverter);
            var register = RegisterTransferData.RegisterTransfers.First();

            var actionResult = await registerController.Register(register);

            Assert.Equal(register.Authorize.Email, actionResult.Value);
        }

        /// <summary>
        /// Зарегистрировать
        /// </summary>
        [Fact]
        public async Task Register_Duplicate()
        {
            var user = IdentityData.BoutiqueRoleUser;
            var userResult = user.ToResultValue();
            var userNotFound = user.ToResultValue();
            var userManager = GetUserManager(userResult, userNotFound);
            var registerController = new RegisterController(userManager.Object, AuthorizeSettings,
                                                            RegisterTransferConverterMock.RegisterTransferConverter);
            var register = RegisterTransferData.RegisterTransfers.First();

            var actionResult = await registerController.Register(register);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(AuthorizeErrorType.Duplicate.ToString(), errors.Keys.First());
        }


        /// <summary>
        /// Зарегистрировать
        /// </summary>
        [Fact]
        public async Task Register_RegisterError()
        {
            var user = IdentityData.BoutiqueRoleUsers.First();
            var userResult = ErrorResultFactory.ValueNotValidError(user, GetType(), "ValueNotValid").ToResultValue<BoutiqueRoleUser>();
            var userNotFound = ErrorResultFactory.ValueNotFoundError(user, GetType()).ToResultValue<BoutiqueRoleUser>();
            var userManager = GetUserManager(userResult, userNotFound);
            var registerController = new RegisterController(userManager.Object, AuthorizeSettings,
                                                            RegisterTransferConverterMock.RegisterTransferConverter);
            var register = RegisterTransferData.RegisterTransfers.First();

            var actionResult = await registerController.Register(register);

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
            var user = IdentityData.BoutiqueRoleUser;
            var userResult = user.ToResultValue();
            var userNotFound = ErrorResultFactory.ValueNotFoundError(user, GetType()).ToResultValue<BoutiqueRoleUser>();
            var userManager = GetUserManager(userResult, userNotFound);
            var registerController = new RegisterController(userManager.Object, AuthorizeSettings,
                                                            RegisterTransferConverterMock.RegisterTransferConverter);
            var authorize = new AuthorizeTransfer("", "testTest07071");
            var register = new RegisterTransfer(authorize, PersonalTransferData.PersonalTransfers.First());

            var actionResult = await registerController.Register(register);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(AuthorizeErrorType.Email.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Зарегистрировать. Ошибка повторной регистрации
        /// </summary>
        [Fact]
        public async Task Register_DuplicateError()
        {
            var user = IdentityData.BoutiqueRoleUser;
            var userResult = user.ToResultValue();
            var userManager = GetUserManager(userResult, userResult);
            var registerController = new RegisterController(userManager.Object, AuthorizeSettings,
                                                            RegisterTransferConverterMock.RegisterTransferConverter);
            var authorize = new AuthorizeTransfer("test@yandex.ru", "testTest07071");
            var register = new RegisterTransfer(authorize, PersonalTransferData.PersonalTransfers.First());
            var actionResult = await registerController.Register(register);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(AuthorizeErrorType.Duplicate.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Менеджер авторизации
        /// </summary>
        private static Mock<IUserManagerService> GetUserManager(IResultValue<BoutiqueRoleUser> userResult,
                                                                 IResultValue<BoutiqueRoleUser> userFound) =>
            new Mock<IUserManagerService>().
            Void(userMock => userMock.Setup(userManager => userManager.CreateRoleUser(It.IsAny<IRegisterDomain>(), It.IsAny<IdentityRoleType>())).
                                      ReturnsAsync(userResult.ResultValueOk(user => user.BoutiqueIdentityUser.Email))).
            Void(userMock => userMock.Setup(userManager => userManager.FindUserByEmail(It.IsAny<string>())).
                                      ReturnsAsync(userFound.ResultValueOk(user => user.BoutiqueIdentityUser)));

        /// <summary>
        /// Параметры авторизации
        /// </summary>
        private static AuthorizeSettings AuthorizeSettings =>
            new(8, true, false, true);
    }
}