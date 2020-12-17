using System.Net;
using BoutiqueDTO.Models.Interfaces.Base;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using RestSharp;

namespace BoutiqueDTO.Extensions.RestResponse
{
    /// <summary>
    /// Методы расширения 
    /// </summary>
    public static class RestResponseExtensions
    {
        public static IResultValue<TValue> ToRestResultError<TValue>(this IRestResponse<TValue> @this)
             where TValue : notnull =>
            @this.StatusCode switch
            {
                HttpStatusCode.OK => new ResultValue<TValue>(@this.Data),
                HttpStatusCode.Accepted => new ResultValue<TValue>(@this.Data),
                HttpStatusCode.BadGateway => new ErrorResult(ErrorResultType.BadGateway, $"Маршрут {@this.ResponseUri} не найден").ToResultValue<TValue>(),
                HttpStatusCode.BadRequest => new ErrorResult(ErrorResultType.BadRequest, $"Некорректный запрос. {@this.ErrorMessage}").ToResultValue<TValue>(),
                HttpStatusCode.GatewayTimeout => new ErrorResult(ErrorResultType.GatewayTimeout, "Время ожидания истекло").ToResultValue<TValue>(),
                HttpStatusCode.InternalServerError => new ErrorResult(ErrorResultType.InternalServerError, $"Ошибка сервера. {@this.ErrorMessage}").ToResultValue<TValue>(),
                HttpStatusCode.NotFound => new ErrorResult(ErrorResultType.ValueNotFound, $"Элемент не найден. {@this.ErrorMessage}").ToResultValue<TValue>(),
                HttpStatusCode.RequestTimeout => new ErrorResult(ErrorResultType.RequestTimeOut, "Время ожидания ответа истекло").ToResultValue<TValue>(),
                HttpStatusCode.Unauthorized => new ErrorResult(ErrorResultType.Unauthorized, "Авторизация не пройдена").ToResultValue<TValue>(),
                _ => new ErrorResult(ErrorResultType.Unknown, "Неизвестный статус ответа сервера").ToResultValue<TValue>(),
            };
    }
}