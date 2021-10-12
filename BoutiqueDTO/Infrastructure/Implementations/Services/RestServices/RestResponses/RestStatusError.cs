using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BoutiqueDTO.Extensions.Json.Sync;
using Newtonsoft.Json;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.RestResponses
{
    /// <summary>
    /// Преобразование rest статуса в результирующую ошибку
    /// </summary>
    public static class RestStatusError
    {
        /// <summary>
        /// Преобразовать статус в результирующую ошибку
        /// </summary>
        public static async Task<IErrorResult> RestStatusToErrorResult(HttpResponseMessage httpResponse) =>
            httpResponse.StatusCode switch
            {
                0 =>
                    ErrorResultFactory.RestError(RestErrorType.ServerNotFound, httpResponse, $"Сервер {httpResponse.RequestMessage.RequestUri} не найден"),
                HttpStatusCode.BadGateway =>
                    ErrorResultFactory.RestError(RestErrorType.BadGateway, httpResponse, $"Маршрут {httpResponse.RequestMessage.RequestUri} не найден"),
                HttpStatusCode.BadRequest =>
                    await GetBadRequestError(httpResponse),
                HttpStatusCode.GatewayTimeout =>
                    ErrorResultFactory.RestError(RestErrorType.GatewayTimeout, httpResponse, "Время ожидания истекло"),
                HttpStatusCode.InternalServerError =>
                    ErrorResultFactory.RestError(RestErrorType.InternalServerError, httpResponse, $"Ошибка сервера. {httpResponse.ReasonPhrase}"),
                HttpStatusCode.NotFound =>
                    ErrorResultFactory.RestError(RestErrorType.ValueNotFound, httpResponse, $"Элемент не найден. {httpResponse.ReasonPhrase}"),
                HttpStatusCode.RequestTimeout =>
                    ErrorResultFactory.RestError(RestErrorType.RequestTimeout, httpResponse, "Время ожидания ответа истекло"),
                HttpStatusCode.RequestEntityTooLarge =>
                    ErrorResultFactory.RestError(RestErrorType.RequestEntityToLarge, httpResponse, "Запрос слишком велик"),
                HttpStatusCode.Unauthorized =>
                    ErrorResultFactory.RestError(RestErrorType.Unauthorized, httpResponse, "Авторизация не пройдена"),
                HttpStatusCode.UnsupportedMediaType =>
                    ErrorResultFactory.RestError(RestErrorType.UnsupportedMediaType, httpResponse, "Недопустимое тело запроса"),
                _ =>
                    ErrorResultFactory.RestError(RestErrorType.UnknownRestStatus, httpResponse, "Неизвестный статус ответа сервера"),
            };

        /// <summary>
        /// Получить ошибку запроса
        /// </summary>
        private static async Task<IErrorResult> GetBadRequestError(HttpResponseMessage httpResponse) =>
            await httpResponse.Content.ReadAsStringAsync().
            MapTaskAsync(content => content.ToTransferValueJson<IDictionary<string, string[]>>()).
            WhereContinueTaskAsync(result => result.OkStatus,
                                   result => ErrorResultFactory.RestError(RestErrorType.BadRequest, httpResponse,
                                                                          GetBadRequestMessage(result.Value)),
                                   result => result.Errors.First());

        /// <summary>
        /// Получить сообщение об ошибке
        /// </summary>
        private static string GetBadRequestMessage(IDictionary<string, string[]> errorDictionary) =>
            errorDictionary.Values.FirstOrDefault()?.FirstOrDefault() ?? String.Empty;

    }
}