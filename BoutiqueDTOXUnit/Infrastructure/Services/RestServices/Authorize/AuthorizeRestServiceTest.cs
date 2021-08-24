using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Authorization;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Authorize;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Authorization;
using BoutiqueDTOXUnit.Data;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Services;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Implementations.Results;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Services.RestServices.Authorize
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
            var error = ErrorTransferData.ErrorTypeBadRequest;
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