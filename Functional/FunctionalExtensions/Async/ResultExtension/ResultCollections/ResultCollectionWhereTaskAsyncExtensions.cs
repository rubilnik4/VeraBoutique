﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Interfaces.Errors;
using Functional.Models.Interfaces.Errors.Base;
using Functional.Models.Interfaces.Results;

namespace Functional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Обработка асинхронных условий для результирующего ответа с коллекцией задачей-объектом
    /// </summary>
    public static class ResultCollectionWhereTaskAsyncExtensions
    {
        /// <summary>
        /// Выполнение условия или возвращение предыдущей ошибки в результирующем ответе с коллекцией задачи-объекта
        /// </summary>      
        public static async Task<IResultCollection<TValueOut>> ResultCollectionContinueTaskAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, IEnumerable<TValueOut>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, IEnumerable<IErrorResult>> badFunc) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultCollectionContinue(predicate, okFunc, badFunc));

        /// <summary>
        /// Выполнение условия или возвращение предыдущей ошибки в результирующем ответе с коллекцией задачи-объекта
        /// </summary>      
        public static async Task<IResultCollection<TValueOut>> ResultCollectionWhereTaskAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, IEnumerable<TValueOut>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, IEnumerable<TValueOut>> badFunc) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultCollectionWhere(predicate, okFunc, badFunc));

        /// <summary>
        /// Выполнение положительного или негативного условия в результирующем ответе с коллекцией задачи-объекта
        /// </summary>      
        public static async Task<IResultCollection<TValueOut>> ResultCollectionOkBadTaskAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                         Func<IReadOnlyCollection<TValueIn>, IEnumerable<TValueOut>> okFunc,
                                                                                                         Func<IReadOnlyCollection<IErrorResult>, IEnumerable<TValueOut>> badFunc) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultCollectionOkBad(okFunc, badFunc));

        /// <summary>
        /// Выполнение положительного условия или возвращение предыдущей ошибки в результирующем ответе с коллекцией задачи-объекта
        /// </summary>   
        public static async Task<IResultCollection<TValueOut>> ResultCollectionOkTaskAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                      Func<IReadOnlyCollection<TValueIn>, IEnumerable<TValueOut>> okFunc) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultCollectionOk(okFunc));

        /// <summary>
        /// Выполнение негативного условия или возвращение положительного условия в результирующем ответе с коллекцией задачи-объекта
        /// </summary>   
        public static async Task<IResultCollection<TValue>> ResultCollectionBadTaskAsync<TValue>(this Task<IResultCollection<TValue>> @this,
                                                                                       Func<IReadOnlyCollection<IErrorResult>, IEnumerable<TValue>> badFunc) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultCollectionBad(badFunc));
    }
}