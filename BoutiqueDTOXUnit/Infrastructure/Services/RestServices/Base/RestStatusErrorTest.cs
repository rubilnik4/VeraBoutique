using System;
using System.Net;
using System.Net.Http;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.RestResponses;
using Functional.Models.Enums;
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
        [InlineData(HttpStatusCode.BadGateway, ErrorResultType.BadGateway)]
        [InlineData(HttpStatusCode.BadRequest, ErrorResultType.BadRequest)]
        [InlineData(HttpStatusCode.GatewayTimeout, ErrorResultType.GatewayTimeout)]
        [InlineData(HttpStatusCode.InternalServerError, ErrorResultType.InternalServerError)]
        [InlineData(HttpStatusCode.NotFound, ErrorResultType.ValueNotFound)]
        [InlineData(HttpStatusCode.RequestTimeout, ErrorResultType.RequestTimeout)]
        [InlineData(HttpStatusCode.Unauthorized, ErrorResultType.Unauthorized)]
        public void HttpStatusCodeStatus(HttpStatusCode httpStatusCode, ErrorResultType errorResultType)
        {
            var httpMessage = new HttpRequestMessage(HttpMethod.Get, "localhost");
            var restResponse = GetRestResponse(httpStatusCode, httpStatusCode.ToString(), httpMessage);

            var errorResult = RestStatusError.RestStatusToErrorResult(restResponse);

            Assert.Equal(errorResultType, errorResult.ErrorType);
        }

        [Fact]
        public void HttpStatusCodeStatusServerNotFound()
        {
            var httpMessage = new HttpRequestMessage(HttpMethod.Get, "localhost");
            var restResponse = GetRestResponse(0, "errorInternal", httpMessage);

            var errorResult = RestStatusError.RestStatusToErrorResult(restResponse);

            Assert.Equal(ErrorResultType.ServerNotFound, errorResult.ErrorType);
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

            Assert.Equal(ErrorResultType.InternalServerError, errorResult.ErrorType);
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