﻿using System.Threading.Tasks;
using BoutiqueDTO.Extensions.Json.Sync;
using BoutiqueDTO.Models.Interfaces.Base;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Results;
using Newtonsoft.Json;

namespace BoutiqueDTO.Extensions.Json.Async
{
    /// <summary>
    /// Асинхронные методы расширения для json
    /// </summary>
    public static class JsonAsyncExtensions
    {
        /// <summary>
        /// Преобразовать json в трансферную модель
        /// </summary>
        public static async Task<IResultValue<TValue>> ToTransferValueJsonAsync<TValue>(this Task<string> json)
            where TValue : notnull =>
            await json.MapTaskAsync(jsonAwaited => jsonAwaited.ToTransferValueJson<TValue>());

        /// <summary>
        /// Преобразовать json в коллекцию трансферных моделей
        /// </summary>
        public static async Task<IResultCollection<TValue>> ToTransferCollectionJsonAsync<TValue>(this Task<string> json)
            where TValue : notnull =>
            await json.MapTaskAsync(jsonAwaited => jsonAwaited.ToTransferCollectionJson<TValue>());
    }
}