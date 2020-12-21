using System.Net;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using RestSharp;

namespace BoutiqueDTO.Infrastructure.Implementations.RestResponse
{
    /// <summary>
    /// Преобразование rest статуса в результирующую ошибку
    /// </summary>
    public static class RestStatusError
    {
        /// <summary>
        /// Преобразовать статус в результирующую ошибку
        /// </summary>
        public static ErrorResult RestStatusToErrorResult(IRestResponse restResponse) =>
            restResponse.StatusCode switch
            {
                HttpStatusCode.BadGateway => new ErrorResult(ErrorResultType.BadGateway, $"Маршрут {restResponse.ResponseUri} не найден"),
                HttpStatusCode.BadRequest => new ErrorResult(ErrorResultType.BadRequest, $"Некорректный запрос. {restResponse.ErrorMessage}"),
                HttpStatusCode.GatewayTimeout => new ErrorResult(ErrorResultType.GatewayTimeout, "Время ожидания истекло"),
                HttpStatusCode.InternalServerError => new ErrorResult(ErrorResultType.InternalServerError, $"Ошибка сервера. {restResponse.ErrorMessage}"),
                HttpStatusCode.NotFound => new ErrorResult(ErrorResultType.ValueNotFound, $"Элемент не найден. {restResponse.ErrorMessage}"),
                HttpStatusCode.RequestTimeout => new ErrorResult(ErrorResultType.RequestTimeOut, "Время ожидания ответа истекло"),
                HttpStatusCode.Unauthorized => new ErrorResult(ErrorResultType.Unauthorized, "Авторизация не пройдена"),
                _ => new ErrorResult(ErrorResultType.UnknownRestStatus, "Неизвестный статус ответа сервера"),
            };
    }
}