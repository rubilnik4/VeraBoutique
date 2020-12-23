using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueDTO.Extensions.RestResponses.Sync;
using Functional.FunctionalExtensions.Async;
using Functional.Models.Interfaces.Result;
using RestSharp;

namespace BoutiqueDTO.Extensions.RestResponses.Async
{
    /// <summary>
    /// Методы расширения ответа Api сервера для задачи-объекта
    /// </summary>
    public static class RestResponseAsyncExtensions
    {
        /// <summary>
        /// Преобразовать ответ сервера в результирующий ответ со значением
        /// </summary>
        public static async Task<IResultValue<TValue>> ToRestResultValueAsync<TValue>(this Task<IRestResponse<TValue>> @this)
             where TValue : notnull =>
            await @this.
            MapTaskAsync(restResponse => restResponse.ToRestResultValue());

        /// <summary>
        /// Преобразовать ответ сервера в результирующий ответ с коллекцией
        /// </summary>
        public static async Task<IResultCollection<TValue>> ToRestResultCollectionAsync<TValue>(this Task<IRestResponse<List<TValue>>> @this)
             where TValue : notnull =>
            await @this.
            MapTaskAsync(restResponse => restResponse.ToRestResultCollection());

        /// <summary>
        /// Преобразовать ответ сервера в результирующий ответ со значением
        /// </summary>
        public static async Task<IResultError> ToRestResultErrorAsync(this Task<IRestResponse> @this) =>
            await @this.
            MapTaskAsync(restResponse => restResponse.ToRestResultError());
    }
}