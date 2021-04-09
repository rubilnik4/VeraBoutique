using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Authorization;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Authorization;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Authorization;
using BoutiqueDTO.Models.Implementations.Identity;
using BoutiqueDTOXUnit.Data;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Services;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Moq;
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
            var authorize = AuthorizeData.AuthorizeDomains.First();
            var restHttpClient = RestClientMock.PostRestClient(jwtTokenResult);
            var authorizeTransferConverter = AuthorizeTransferConverter;
            var authorizeRestService = new AuthorizeRestService(restHttpClient.Object, authorizeTransferConverter);

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
            var authorize = AuthorizeData.AuthorizeDomains.First();
            var restHttpClient = RestClientMock.PostRestClient(jwtTokenResult);
            var authorizeTransferConverter = AuthorizeTransferConverter;
            var authorizeRestService = new AuthorizeRestService(restHttpClient.Object, authorizeTransferConverter);

            var resultToken = await authorizeRestService.AuthorizeJwt(authorize);

            Assert.True(resultToken.HasErrors);
            Assert.True(error.Equals(resultToken.Errors.First()));
        }

        /// <summary>
        /// Конвертер логина и пароля в трансферную модель
        /// </summary>
        private static IAuthorizeTransferConverter AuthorizeTransferConverter =>
            new AuthorizeTransferConverter();
    }
}