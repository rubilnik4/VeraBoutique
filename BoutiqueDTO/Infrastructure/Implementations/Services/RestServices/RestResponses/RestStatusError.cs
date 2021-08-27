using System.Net;
using System.Net.Http;
using Functional.Models.Enums;
using Functional.Models.Implementations.Errors;
using Functional.Models.Implementations.Errors.Base;
using Functional.Models.Interfaces.Errors;
using Functional.Models.Interfaces.Errors.Base;

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
        public static IErrorResult RestStatusToErrorResult(HttpResponseMessage httpResponse) =>
            httpResponse.StatusCode switch
            {
                0 =>
                    ErrorResultFactory.RestError(RestErrorType.ServerNotFound, httpResponse, $"Сервер {httpResponse.RequestMessage.RequestUri} не найден"),
                HttpStatusCode.BadGateway =>
                    ErrorResultFactory.RestError(RestErrorType.BadGateway, httpResponse, $"Маршрут {httpResponse.RequestMessage.RequestUri} не найден"),
                HttpStatusCode.BadRequest =>
                    ErrorResultFactory.RestError(RestErrorType.BadRequest, httpResponse, $"Некорректный запрос. {httpResponse.ReasonPhrase}"),
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
    }
}