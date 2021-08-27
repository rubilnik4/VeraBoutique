using System;
using System.Net;
using System.Net.Http;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.RestResponses;
using Functional.Models.Enums;
using Functional.Models.Implementations.Errors.RestErrors;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Services.RestServices.Base
{
    /// <summary>
    /// Преобразование rest статуса в результирующую ошибку. Тесты
    /// </summary>
    public class RestStatusErrorTest
    {
        /// <summary>
        /// Статус ошибки ответа
        /// </summary>
        [Theory]
        [InlineData(HttpStatusCode.BadGateway, RestErrorType.BadGateway)]
        [InlineData(HttpStatusCode.BadRequest, RestErrorType.BadRequest)]
        [InlineData(HttpStatusCode.GatewayTimeout, RestErrorType.GatewayTimeout)]
        [InlineData(HttpStatusCode.InternalServerError, RestErrorType.InternalServerError)]
        [InlineData(HttpStatusCode.NotFound, RestErrorType.ValueNotFound)]
        [InlineData(HttpStatusCode.RequestTimeout, RestErrorType.RequestTimeout)]
        [InlineData(HttpStatusCode.Unauthorized, RestErrorType.Unauthorized)]
        public void HttpStatusCodeStatus(HttpStatusCode httpStatusCode, RestErrorType errorResultType)
        {
            var httpMessage = new HttpRequestMessage(HttpMethod.Get, "localhost");
            var restResponse = GetRestResponse(httpStatusCode, httpStatusCode.ToString(), httpMessage);

            var errorResult = RestStatusError.RestStatusToErrorResult(restResponse);

            Assert.IsType<RestMessageErrorResult>(errorResult);
            Assert.Equal(errorResultType, ((RestMessageErrorResult)errorResult).ErrorType);
        }

        [Fact]
        public void HttpStatusCodeStatusServerNotFound()
        {
            var httpMessage = new HttpRequestMessage(HttpMethod.Get, "localhost");
            var restResponse = GetRestResponse(0, "errorInternal", httpMessage);

            var errorResult = RestStatusError.RestStatusToErrorResult(restResponse);

            Assert.IsType<RestMessageErrorResult>(errorResult);
            Assert.Equal(RestErrorType.ServerNotFound, ((RestMessageErrorResult)errorResult).ErrorType);
        }

        /// <summary>
        /// Проверка внутренней ошибки сервера
        /// </summary>
        [Fact]
        public void InternalServerErrorException()
        {
            var httpMessage = new HttpRequestMessage(HttpMethod.Get, "localhost");
            var restResponse = GetRestResponse(HttpStatusCode.InternalServerError, "errorInternal", httpMessage);

            var errorResult = RestStatusError.RestStatusToErrorResult(restResponse);

            Assert.Equal(RestErrorType.InternalServerError, ((RestMessageErrorResult)errorResult).ErrorType);
        }

        /// <summary>
        /// Создать ответ сервера
        /// </summary>
        private static HttpResponseMessage GetRestResponse(HttpStatusCode httpStatusCode, string? reasonPhrase,
                                                           HttpRequestMessage? httpRequestMessage) =>
            new()
            {
                StatusCode = httpStatusCode,
                ReasonPhrase = reasonPhrase,
                RequestMessage = httpRequestMessage,
            };
    }
}