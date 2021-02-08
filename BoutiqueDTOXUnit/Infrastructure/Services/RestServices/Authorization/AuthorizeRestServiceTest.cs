using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Authorization;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Authorization;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Authorization;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Authorization;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Authorization;
using BoutiqueDTO.Models.Implementations.Identity;
using BoutiqueDTOXUnit.Data;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Moq;
using RestSharp;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Services.RestServices.Authorization
{
    /// <summary>
    /// Сервис авторизации. Тесты
    /// </summary>
    public class AuthorizeRestServiceTest
    {
        /// <summary>
        /// Авторизироваться в сервисе
        /// </summary>
        [Fact]
        public async Task AuthorizeJwt_Ok()
        {
            const string jwtToken = "jwtToken";
            var jwtTokenResult = jwtToken.ToResultValue();
            var authorize = AuthorizeData.AuthorizeDomain.First();
            var authorizeApiService = GetAuthorizeApiService(jwtTokenResult);
            var authorizeTransferConverter = AuthorizeTransferConverter;
            var authorizeRestService = new AuthorizeRestService(authorizeApiService.Object, authorizeTransferConverter);

            var resultToken = await authorizeRestService.AuthorizeJwt(authorize);

            Assert.True(resultToken.OkStatus);
            Assert.Equal(jwtToken, resultToken.Value);
        }

        /// <summary>
        /// Авторизироваться в сервисе. Ошибка
        /// </summary>
        [Fact]
        public async Task AuthorizeJwt_Error()
        {
            var error = ErrorTransferData.ErrorBadRequest;
            var jwtTokenResult = new ResultValue<string>(error);
            var authorize = AuthorizeData.AuthorizeDomain.First();
            var authorizeApiService = GetAuthorizeApiService(jwtTokenResult);
            var authorizeTransferConverter = AuthorizeTransferConverter;
            var authorizeRestService = new AuthorizeRestService(authorizeApiService.Object, authorizeTransferConverter);

            var resultToken = await authorizeRestService.AuthorizeJwt(authorize);

            Assert.True(resultToken.HasErrors);
            Assert.True(error.Equals(resultToken.Errors.First()));
        }

        /// <summary>
        /// Получить Api сервис авторизации
        /// </summary>
        private static Mock<IAuthorizeApiService> GetAuthorizeApiService(IResultValue<string> jwtToken) =>
            new Mock<IAuthorizeApiService>().
            Void(mock => mock.Setup(service => service.AuthorizeJwt(It.IsAny<AuthorizeTransfer>())).
                              ReturnsAsync(jwtToken));

        /// <summary>
        /// Конвертер логина и пароля в трансферную модель
        /// </summary>
        private static IAuthorizeTransferConverter AuthorizeTransferConverter =>
            new AuthorizeTransferConverter();

        /// <summary>
        /// Отображение сообщений
        /// </summary>
        private static Mock<IBoutiqueLogger> BoutiqueLogger =>
           new();
    }
}