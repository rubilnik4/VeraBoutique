﻿using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Identity;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Authorize;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Identity;
using BoutiqueDTOXUnit.Data;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Identity;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Services;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Errors.AuthorizeErrors;
using ResultFunctional.Models.Implementations.Errors.RestErrors;
using ResultFunctional.Models.Implementations.Results;
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
            var authorizeTransferConverter = AuthorizeTransferConverterMock.AuthorizeTransferConverter;
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
            var error = ErrorTransferData.ErrorTypeAuthorizeError;
            var jwtTokenResult = new ResultValue<string>(error);
            var authorize = AuthorizeData.AuthorizeDomains.First();
            var restHttpClient = RestClientMock.PostRestClient(jwtTokenResult);
            var authorizeTransferConverter = AuthorizeTransferConverterMock.AuthorizeTransferConverter;
            var authorizeRestService = new AuthorizeRestService(restHttpClient.Object, authorizeTransferConverter);

            var resultToken = await authorizeRestService.AuthorizeJwt(authorize);

            Assert.True(resultToken.HasErrors);
            Assert.IsType<RestMessageErrorResult>(resultToken.Errors.First());
            Assert.Equal(resultToken.Errors.First().Id, error.Id);
        }

        /// <summary>
        /// Авторизироваться в сервисе. Ошибка
        /// </summary>
        [Fact]
        public async Task AuthorizeJwt_InternalError()
        {
            var error = ErrorTransferData.ErrorTypeInternalError;
            var jwtTokenResult = new ResultValue<string>(error);
            var authorize = AuthorizeData.AuthorizeDomains.First();
            var restHttpClient = RestClientMock.PostRestClient(jwtTokenResult);
            var authorizeTransferConverter = AuthorizeTransferConverterMock.AuthorizeTransferConverter;
            var authorizeRestService = new AuthorizeRestService(restHttpClient.Object, authorizeTransferConverter);

            var resultToken = await authorizeRestService.AuthorizeJwt(authorize);

            Assert.True(resultToken.HasErrors);
            Assert.IsType<RestMessageErrorResult>(resultToken.Errors.First());
            Assert.Equal(resultToken.Errors.First().Id, error.Id);
        }
    }
}