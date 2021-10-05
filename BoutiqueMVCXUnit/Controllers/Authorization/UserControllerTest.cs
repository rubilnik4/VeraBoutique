using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueDAL.Infrastructure.Interfaces.Identity;
using BoutiqueDAL.Models.Implementations.Identity;
using BoutiqueMVC.Controllers.Implementations.Identity;
using BoutiqueMVCXUnit.Data.Identity;
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
            var userController = new UserController(userManager.Object);

            var usersResult = await userController.GetRoleUsers();

            Assert.True(usersResult.Value.Zip(users).
                                    All(user => user.Second.EqualToBoutiqueUser(user.First)));
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
            var userController = new UserController(userManager.Object);

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
            var userController = new UserController(userManager.Object);

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
            var userController = new UserController(userManager.Object);

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
        private static Mock<IUserManagerBoutique> GetUserManager(IReadOnlyCollection<BoutiqueRoleUser> users) =>
            new Mock<IUserManagerBoutique>().
            Void(userMock => userMock.Setup(userManager => userManager.GetRoleUsers()).
                                      ReturnsAsync(users));

        /// <summary>
        /// Менеджер авторизации
        /// </summary>
        private static Mock<IUserManagerBoutique> GetUserManager(IResultValue<BoutiqueRoleUser> userResult) =>
            new Mock<IUserManagerBoutique>().
            Void(userMock => userMock.Setup(userManager => userManager.FindUserByEmail(It.IsAny<string>())).
                                      ReturnsAsync(userResult.ResultValueOk(user => user.BoutiqueIdentityUser))).
            Void(userMock => userMock.Setup(userManager => userManager.DeleteRoleUser(It.IsAny<BoutiqueIdentityUser>())).
                                      ReturnsAsync(userResult.ResultValueOk(user => user.BoutiqueIdentityUser.Email)));
    }
}