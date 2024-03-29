﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BoutiqueDTO.Extensions.RestResponses.Sync;
using BoutiqueDTO.Models.Interfaces.Base;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDTO.Extensions.RestResponses.Async
{
    /// <summary>
    /// Методы расширения ответа Api сервера для задачи-объекта
    /// </summary>
    public static class RestResponseTaskAsyncExtensions
    {
        /// <summary>
        /// Преобразовать ответ сервера в результирующий ответ со значением
        /// </summary>
        public static async Task<IResultValue<string>> ToRestResultTaskAsync(this Task<HttpResponseMessage> @this) =>
            await @this.
            MapBindAsync(restResponse => restResponse.ToRestResultAsync());

        /// <summary>
        /// Преобразовать ответ сервера в результирующий ответ со значением
        /// </summary>
        public static async Task<IResultValue<TValue>> ToRestResultValueTaskAsync<TValue>(this Task<HttpResponseMessage> @this)
            where TValue : notnull =>
            await @this.
            MapBindAsync(restResponse => restResponse.ToRestResultValueAsync<TValue>());

        /// <summary>
        /// Преобразовать ответ сервера в результирующий ответ с коллекцией
        /// </summary>
        public static async Task<IResultCollection<TValue>> ToRestResultCollectionTaskAsync<TValue>(this Task<HttpResponseMessage> @this)
            where TValue : notnull =>
            await @this.
            MapBindAsync(restResponse => restResponse.ToRestResultCollectionAsync<TValue>());

        /// <summary>
        /// Преобразовать ответ сервера в результирующий ответ со значением
        /// </summary>
        public static async Task<IResultError> ToRestResultErrorTaskAsync(this Task<HttpResponseMessage> @this) =>
            await @this.
            MapBindAsync(restResponse => restResponse.ToRestResultError());
    }
}