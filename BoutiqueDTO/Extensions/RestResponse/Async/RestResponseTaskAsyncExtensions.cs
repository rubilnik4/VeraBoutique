using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using BoutiqueDTO.Extensions.RestResponse.Sync;
using BoutiqueDTO.Infrastructure.Implementations.RestResponse;
using Functional.FunctionalExtensions.Async;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using RestSharp;

namespace BoutiqueDTO.Extensions.RestResponse.Async
{
    /// <summary>
    /// Методы расширения ответа Api сервера для задачи-объекта
    /// </summary>
    public static class RestResponseTaskAsyncExtensions
    {
        /// <summary>
        /// Преобразовать ответ сервера в результирующий ответ со значением
        /// </summary>
        public static async Task<IResultValue<TValue>> ToRestResultValueTaskAsync<TValue>(this Task<IRestResponse<TValue>> @this)
             where TValue : notnull =>
            await @this.
            MapTaskAsync(restResponse => restResponse.ToRestResultValue());

        /// <summary>
        /// Преобразовать ответ сервера в результирующий ответ с коллекцией
        /// </summary>
        public static async Task<IResultCollection<TValue>> ToRestResultCollectionTaskAsync<TValue>(this Task<IRestResponse<List<TValue>>> @this)
             where TValue : notnull =>
            await @this.
            MapTaskAsync(restResponse => restResponse.ToRestResultCollection());

        /// <summary>
        /// Преобразовать ответ сервера в результирующий ответ со значением
        /// </summary>
        public static async Task<IResultError> ToRestResultErrorTaskAsync(this Task<IRestResponse> @this) =>
            await @this.
            MapTaskAsync(restResponse => restResponse.ToRestResultError());
    }
}