using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Identities;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Identity;
using BoutiqueMVC.Controllers.Identity;
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
    /// Контроллер профиля. Тесты
    /// </summary>
    public class ProfileControllerTest
    {
        /// <summary>
        /// Получить профиль
        /// </summary>
        [Fact]
        public async Task GetProfile()
        {
            var user = IdentityData.BoutiqueUsers.First();
            var resultUser = user.ToResultValue();
            var userManager = GetUserManager(resultUser);
            var claimUser = GetClaimsIdentity(user.Email);
            var httpContext = new DefaultHttpContext { User = claimUser };
            var profileController = new ProfileController(userManager.Object, BoutiqueUserTransferConverterMock.BoutiqueUserTransferConverter)
            {
                ControllerContext = new ControllerContext { HttpContext = httpContext }
            };

            var profileResult = await profileController.GetProfile();

            Assert.True(profileResult.Value.Equals(user));
        }

        /// <summary>
        /// Получить профиль
        /// </summary>
        [Fact]
        public async Task GetProfile_Unauthorized()
        {
            var user = IdentityData.BoutiqueUsers.First();
            var resultUser = user.ToResultValue();
            var userManager = GetUserManager(resultUser);
            var httpContext = new DefaultHttpContext { User = null! };
            var profileController = new ProfileController(userManager.Object, BoutiqueUserTransferConverterMock.BoutiqueUserTransferConverter)
            {
                ControllerContext = new ControllerContext { HttpContext = httpContext }
            };

            var profileResult = await profileController.GetProfile();

            Assert.IsType<UnauthorizedResult>(profileResult.Result);
        }

        /// <summary>
        /// Получить профиль
        /// </summary>
        [Fact]
        public async Task GetProfile_BadRequest()
        {
            var user = IdentityData.BoutiqueUsers.First();
            var resultUser = ErrorResultFactory.ValueNotValidError(user,GetType(), "ValueNotValidError").ToResultValue<IBoutiqueUserDomain>();
            var userManager = GetUserManager(resultUser);
            var claimUser = GetClaimsIdentity(user.Email);
            var httpContext = new DefaultHttpContext { User = claimUser };
            var profileController = new ProfileController(userManager.Object, BoutiqueUserTransferConverterMock.BoutiqueUserTransferConverter)
            {
                ControllerContext = new ControllerContext { HttpContext = httpContext }
            };

            var profileResult = await profileController.GetProfile();

            Assert.IsType<UnauthorizedResult>(profileResult.Result);
        }

        /// <summary>
        /// Пользователь в контроллере
        /// </summary>
        private static ClaimsPrincipal GetClaimsIdentity(string email) =>
             new Claim(ClaimTypes.NameIdentifier, email).
             Map(claim => new List<Claim> { claim }).
             Map(claims => new ClaimsIdentity(claims)).
             Map(claimIdentity => new ClaimsPrincipal(claimIdentity));

        /// <summary>
        /// Менеджер авторизации
        /// </summary>
        private static Mock<IUserManagerService> GetUserManager(IResultValue<IBoutiqueUserDomain> resultUser) =>
            new Mock<IUserManagerService>().
            Void(userMock => userMock.Setup(userManager => userManager.FindRoleUserByEmail(It.IsAny<string>())).
                                      ReturnsAsync(resultUser));
    }
}