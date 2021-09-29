using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueDAL.Models.Implementations.Identity;
using BoutiqueDAL.Models.Interfaces.Identity;
using BoutiqueDTO.Models.Implementations.Identity;
using BoutiqueDTO.Models.Interfaces.Identity;
using BoutiqueDTOXUnit.Data.Transfers.Authorize;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Identity;
using BoutiqueMVC.Controllers.Implementations.Identity;
using BoutiqueMVC.Models.Implementations.Identity;
using BoutiqueMVCXUnit.Data.Controllers.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MockQueryable.Moq;
using Moq;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.AuthorizeErrors;
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
            var userManager = GetUserManager(IdentityResult.Success);
            var registerController = new RegisterController(userManager.Object, AuthorizeSettings,
                                                            RegisterTransferConverterMock.RegisterTransferConverter);
            var authorize = new AuthorizeTransfer("test@yandex.ru", "testTest07071");
            var register = new RegisterTransfer(authorize, PersonalTransferData.PersonalTransfers.First());
            var actionResult = await registerController.Register(register);

            Assert.IsType<NoContentResult>(actionResult);
            var noContentResult = (NoContentResult)actionResult;
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }

        /// <summary>
        /// Зарегистрировать
        /// </summary>
        [Fact]
        public async Task Register_RegisterError()
        {
            var identityError = new IdentityError { Code = "100", Description = "IdentityError" };
            var userManager = GetUserManager(IdentityResult.Failed(identityError));
            var registerController = new RegisterController(userManager.Object, AuthorizeSettings,
                                                            RegisterTransferConverterMock.RegisterTransferConverter);
            var authorize = new AuthorizeTransfer("test@yandex.ru", "testTest07071");
            var register = new RegisterTransfer(authorize, PersonalTransferData.PersonalTransfers.First());
            var actionResult = await registerController.Register(register);

            Assert.IsType<BadRequestObjectResult>(actionResult);
            var badRequest = (BadRequestObjectResult)actionResult;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(AuthorizeErrorType.Register.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Зарегистрировать. Ошибка при проверке
        /// </summary>
        [Fact]
        public async Task Register_ValidateError()
        {
            var userManager = GetUserManager(IdentityResult.Success);
            var registerController = new RegisterController(userManager.Object, AuthorizeSettings,
                                                            RegisterTransferConverterMock.RegisterTransferConverter);
            var authorize = new AuthorizeTransfer("", "testTest07071");
            var register = new RegisterTransfer(authorize, PersonalTransferData.PersonalTransfers.First());
            var actionResult = await registerController.Register(register);

            Assert.IsType<BadRequestObjectResult>(actionResult);
            var badRequest = (BadRequestObjectResult)actionResult;
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
            var register = RegisterTransferData.RegisterTransfers.First();
            var registerDomain = RegisterData.RegisterDomains.First();
            var userManager = GetUserManager(IdentityResult.Success, BoutiqueUser.GetBoutiqueUser(registerDomain));
            var registerController = new RegisterController(userManager.Object, AuthorizeSettings,
                                                            RegisterTransferConverterMock.RegisterTransferConverter);

            var actionResult = await registerController.Register(register);

            Assert.IsType<BadRequestObjectResult>(actionResult);
            var badRequest = (BadRequestObjectResult)actionResult;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(AuthorizeErrorType.Duplicate.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Менеджер авторизации
        /// </summary>
        private static Mock<IUserManagerBoutique> GetUserManager(IdentityResult identityResult) =>
            GetUserManager(identityResult, null);

        /// <summary>
        /// Менеджер авторизации
        /// </summary>
        private static Mock<IUserManagerBoutique> GetUserManager(IdentityResult identityResult, BoutiqueUser? boutiqueUser) =>
            new Mock<IUserManagerBoutique>().
            Void(userMock => userMock.Setup(userManager => userManager.Register(It.IsAny<IRegisterDomain>())).
                                      ReturnsAsync(identityResult)).
            Void(userMock => userMock.Setup(userManager => userManager.FindByEmail(It.IsAny<string>())).
                                      ReturnsAsync(boutiqueUser));

        /// <summary>
        /// Параметры авторизации
        /// </summary>
        private static AuthorizeSettings AuthorizeSettings =>
            new(8, true, false, true);
    }
}