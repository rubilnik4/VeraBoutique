using System;
using System.Net;
using System.Net.Http;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.RestResponses;
using Functional.Models.Enums;
using Newtonsoft.Json.Serialization;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Services.Api.RestResponses
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
            var restResponse = GetRestResponse(httpStatusCode);

            var errorResult = RestStatusError.RestStatusToErrorResult(restResponse);

            Assert.Equal(errorResultType, errorResult.ErrorResultType);
        }

        [Fact]
        public void HttpStatusCodeStatusServerNotFound()
        {
            var restResponse = GetRestResponse(0);

            var errorResult = RestStatusError.RestStatusToErrorResult(restResponse);

            Assert.Equal(ErrorResultType.ServerNotFound, errorResult.ErrorResultType);
        }

        /// <summary>
        /// Проверка внутренней ошибки сервера
        /// </summary>
        [Fact]
        public void InternalServerErrorException()
        {
            var exception = new ArgumentNullException();
            var restResponse = GetRestResponse(HttpStatusCode.InternalServerError, exception.Message);

            var errorResult = RestStatusError.RestStatusToErrorResult(restResponse);

            Assert.Equal(ErrorResultType.InternalServerError, errorResult.ErrorResultType);
            Assert.IsType<ArgumentNullException>(errorResult.Exception);
        }

        /// <summary>
        /// Создать ответ сервера
        /// </summary>
        private static HttpResponseMessage GetRestResponse(HttpStatusCode httpStatusCode) =>
            GetRestResponse(httpStatusCode, null);

        /// <summary>
        /// Создать ответ сервера
        /// </summary>
        private static HttpResponseMessage GetRestResponse(HttpStatusCode httpStatusCode, string reasonPhrase) =>
            new()
            {
                StatusCode = httpStatusCode,
                ReasonPhrase = reasonPhrase,
            };
    }
}