using System.Collections.Generic;
using System.Net;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.RestResponses;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using RestSharp;

namespace BoutiqueDTO.Extensions.RestResponses.Sync
{
    /// <summary>
    /// Методы расширения ответа Api сервера
    /// </summary>
    public static class RestResponseExtensions
    {
        /// <summary>
        /// Преобразовать ответ сервера в результирующий ответ со значением
        /// </summary>
        public static IResultValue<TValue> ToRestResultValue<TValue>(this IRestResponse<TValue> @this)
             where TValue : notnull =>
            @this.StatusCode switch
            {
                HttpStatusCode.OK => new ResultValue<TValue>(@this.Data),
                HttpStatusCode.Created => new ResultValue<TValue>(@this.Data),
                _ => RestStatusError.RestStatusToErrorResult(@this).ToResultValue<TValue>(),
            };

        /// <summary>
        /// Преобразовать ответ сервера в результирующий ответ с коллекцией
        /// </summary>
        public static IResultCollection<TValue> ToRestResultCollection<TValue>(this IRestResponse<List<TValue>> @this)
             where TValue : notnull =>
            @this.StatusCode switch
            {
                HttpStatusCode.OK => new ResultCollection<TValue>(@this.Data),
                HttpStatusCode.Created => new ResultCollection<TValue>(@this.Data),
                _ => RestStatusError.RestStatusToErrorResult(@this).ToResultCollection<TValue>(),
            };

        /// <summary>
        /// Преобразовать ответ сервера в результирующий ответ со значением
        /// </summary>
        public static IResultError ToRestResultError(this IRestResponse @this) =>
            @this.StatusCode switch
            {
                HttpStatusCode.NoContent => new ResultError(),
                _ => RestStatusError.RestStatusToErrorResult(@this).ToResult(),
            };
    }
}