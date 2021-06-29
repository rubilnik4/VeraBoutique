using System.Net;
using System.Net.Http;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

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
                0 => new ErrorResult(ErrorResultType.ServerNotFound, $"Сервер {httpResponse.RequestMessage.RequestUri} не найден"),
                HttpStatusCode.BadGateway => new ErrorResult(ErrorResultType.BadGateway, $"Маршрут {httpResponse.RequestMessage.RequestUri} не найден"),
                HttpStatusCode.BadRequest => new ErrorResult(ErrorResultType.BadRequest, $"Некорректный запрос. {httpResponse.ReasonPhrase}"),
                HttpStatusCode.GatewayTimeout => new ErrorResult(ErrorResultType.GatewayTimeout, "Время ожидания истекло"),
                HttpStatusCode.InternalServerError => new ErrorResult(ErrorResultType.InternalServerError, $"Ошибка сервера. {httpResponse.ReasonPhrase}"),
                HttpStatusCode.NotFound => new ErrorResult(ErrorResultType.ValueNotFound, $"Элемент не найден. {httpResponse.ReasonPhrase}"),
                HttpStatusCode.RequestTimeout => new ErrorResult(ErrorResultType.RequestTimeout, "Время ожидания ответа истекло"),
                HttpStatusCode.RequestEntityTooLarge => new ErrorResult(ErrorResultType.RequestEntityToLarge, "Запрос слишком велик"),
                HttpStatusCode.Unauthorized => new ErrorResult(ErrorResultType.Unauthorized, "Авторизация не пройдена"),
                HttpStatusCode.UnsupportedMediaType => new ErrorResult(ErrorResultType.UnsupportedMediaType, "Недопустимое тело запроса"),
                _ => new ErrorResult(ErrorResultType.UnknownRestStatus, "Неизвестный статус ответа сервера"),
            };
    }
}