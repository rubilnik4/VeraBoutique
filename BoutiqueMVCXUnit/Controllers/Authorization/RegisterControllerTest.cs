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
using BoutiqueDALXUnit.Data.Identity;
using BoutiqueDTO.Models.Implementations.Identities;
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