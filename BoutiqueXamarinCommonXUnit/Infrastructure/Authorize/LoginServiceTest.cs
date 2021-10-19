using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize;
using BoutiqueXamarinCommon.Infrastructure.Implementations.Authorize;
using BoutiqueXamarinCommon.Infrastructure.Interfaces.Authorize;
using Moq;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;
using Xunit;

namespace BoutiqueXamarinCommonXUnit.Infrastructure.Authorize
{
    /// <summary>
    /// Сервис авторизации и сохранения логина. Тесты
    /// </summary>
    public class LoginServiceTest
    {
        /// <summary>
        /// Авторизироваться
        /// </summary>
        [Fact]
        public async Task Login()
        {
            var authorize = AuthorizeData.AuthorizeDomains.First();
            const string token = "Token";
            var resultToken = token.ToResultValue();
            var authorizeRestService = GetAuthorizeRestService(resultToken);
            var resultStore = new ResultError();
            var loginStore = GetLoginStore(resultStore);
            var loginService = new LoginService(authorizeRestService.Object, loginStore.Object);

            var result = await loginService.Login(authorize);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Авторизироваться
        /// </summary>
        [Fact]
        public async Task Login_RestError()
        {
            var authorize = AuthorizeData.AuthorizeDomains.First();
            var resultToken = ErrorResultFactory.AuthorizeError(AuthorizeErrorType.Token, "Token").ToResultValue<string>();
            var authorizeRestService = GetAuthorizeRestService(resultToken);
            var resultStore = new ResultError();
            var loginStore = GetLoginStore(resultStore);
            var loginService = new LoginService(authorizeRestService.Object, loginStore.Object);

            var result = await loginService.Login(authorize);

            Assert.True(result.HasErrors);
            Assert.Equal(resultToken.Errors.First().Id, result.Errors.First().Id);
        }

        /// <summary>
        /// Авторизироваться
        /// </summary>
        [Fact]
        public async Task Login_StoreError()
        {
            var authorize = AuthorizeData.AuthorizeDomains.First();
            const string token = "Token";
            var resultToken = token.ToResultValue();
            var authorizeRestService = GetAuthorizeRestService(resultToken);
            var resultStore = ErrorResultFactory.AuthorizeError(AuthorizeErrorType.Token, "Token").ToResultValue<string>();
            var loginStore = GetLoginStore(resultStore);
            var loginService = new LoginService(authorizeRestService.Object, loginStore.Object);

            var result = await loginService.Login(authorize);

            Assert.True(result.HasErrors);
            Assert.Equal(resultStore.Errors.First().Id, result.Errors.First().Id);
        }

        /// <summary>
        /// Сервис авторизации
        /// </summary>
        private static Mock<IAuthorizeRestService> GetAuthorizeRestService(IResultValue<string> resultToken) =>
            new Mock<IAuthorizeRestService>().
            Void(serviceMock => serviceMock.Setup(service => service.AuthorizeJwt(It.IsAny<IAuthorizeDomain>())).
                                            ReturnsAsync(resultToken));

        /// <summary>
        /// Хранение и загрузка данных аутентификации
        /// </summary>
        private static Mock<ILoginStore> GetLoginStore(IResultError resultStore) =>
            new Mock<ILoginStore>().
            Void(serviceMock => serviceMock.Setup(service => service.SaveToken(It.IsAny<string>())).
                                            ReturnsAsync(resultStore));
    }
}