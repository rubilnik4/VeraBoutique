using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BoutiqueDTO.Extensions.Json.Sync;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.RestResponses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Errors.RestErrors;
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
        [InlineData(HttpStatusCode.GatewayTimeout, RestErrorType.GatewayTimeout)]
        [InlineData(HttpStatusCode.InternalServerError, RestErrorType.InternalServerError)]
        [InlineData(HttpStatusCode.NotFound, RestErrorType.ValueNotFound)]
        [InlineData(HttpStatusCode.RequestTimeout, RestErrorType.RequestTimeout)]
        [InlineData(HttpStatusCode.Unauthorized, RestErrorType.Unauthorized)]
        public async Task HttpStatusCodeStatus(HttpStatusCode httpStatusCode, RestErrorType errorResultType)
        {
            var httpMessage = new HttpRequestMessage(HttpMethod.Get, "localhost");
            var restResponse = GetRestResponse(httpStatusCode, httpStatusCode.ToString(), httpMessage);

            var errorResult = await RestStatusError.RestStatusToErrorResult(restResponse);

            Assert.IsType<RestMessageErrorResult>(errorResult);
            Assert.Equal(errorResultType, ((RestMessageErrorResult)errorResult).ErrorType);
        }

        /// <summary>
        /// Статус ошибки ответа
        /// </summary>
        [Fact]
        public async Task HttpStatusCodeStatusBadRequest()
        {
            const string description = "Дублирование";
            var authorizeResult = ErrorResultFactory.AuthorizeError(AuthorizeErrorType.Duplicate, description);
            var modelStateDictionary = new ModelStateDictionary();
            modelStateDictionary.AddModelError(authorizeResult.Id, authorizeResult.Description);
            var actionResult = new BadRequestObjectResult(modelStateDictionary);
            var httpMessage = new HttpRequestMessage(HttpMethod.Get, "localhost");
            var restResponse = GetRestResponse(HttpStatusCode.BadRequest, HttpStatusCode.BadRequest.ToString(), httpMessage);
            restResponse.Content = new StringContent(actionResult.Value.ToJsonTransfer().Value);
            var errorResult = await RestStatusError.RestStatusToErrorResult(restResponse);

            Assert.IsType<RestMessageErrorResult>(errorResult);
            Assert.Equal(RestErrorType.BadRequest, ((RestMessageErrorResult)errorResult).ErrorType);
            Assert.Equal(description, errorResult.Description);
        }

        [Fact]
        public async Task HttpStatusCodeStatusServerNotFound()
        {
            var httpMessage = new HttpRequestMessage(HttpMethod.Get, "localhost");
            var restResponse = GetRestResponse(0, "errorInternal", httpMessage);

            var errorResult = await RestStatusError.RestStatusToErrorResult(restResponse);

            Assert.IsType<RestMessageErrorResult>(errorResult);
            Assert.Equal(RestErrorType.ServerNotFound, ((RestMessageErrorResult)errorResult).ErrorType);
        }

        /// <summary>
        /// Проверка внутренней ошибки сервера
        /// </summary>
        [Fact]
        public async Task InternalServerErrorException()
        {
            var httpMessage = new HttpRequestMessage(HttpMethod.Get, "localhost");
            var restResponse = GetRestResponse(HttpStatusCode.InternalServerError, "errorInternal", httpMessage);

            var errorResult = await RestStatusError.RestStatusToErrorResult(restResponse);

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