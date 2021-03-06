﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Async.ResultExtension.ResultValue
{
    /// <summary>
    /// Обработка условий для результирующего ответа с значением с возвращением к коллекции для задачи-объекта
    /// </summary>
    public static class ResultValueWhereToCollectionTaskAsyncExtensions
    {
        /// <summary>
        /// Выполнение условия или возвращение предыдущей ошибки в результирующем ответе значения с возвращением к коллекции
        /// </summary>      
        public static async Task<IResultCollection<TValueOut>> ResultValueContinueToCollectionTaskAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                       Func<TValueIn, bool> predicate,
                                                                                       Func<TValueIn, IEnumerable<TValueOut>> okFunc,
                                                                                       Func<TValueIn, IEnumerable<IErrorResult>> badFunc) =>
            await @this.
            ResultValueContinueTaskAsync(predicate, okFunc, badFunc).
            ToResultCollectionTaskAsync();

        /// <summary>
        /// Выполнение положительного или негативного условия в результирующем ответе значения с возвращением к коллекции
        /// </summary>      
        public static async Task<IResultCollection<TValueOut>> ResultValueOkBadToCollectionTaskAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                    Func<TValueIn, IEnumerable<TValueOut>> okFunc,
                                                                                    Func<IReadOnlyCollection<IErrorResult>, IEnumerable<TValueOut>> badFunc) =>
            await @this.
            ResultValueOkBadTaskAsync(okFunc, badFunc).
            ToResultCollectionTaskAsync();

        /// <summary>
        /// Выполнение положительного условия или возвращение предыдущей ошибки в результирующем ответе значения с возвращением к коллекции
        /// </summary>   
        public static async Task<IResultCollection<TValueOut>> ResultValueOkToCollectionTaskAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                             Func<TValueIn, IEnumerable<TValueOut>> okFunc) =>
            await @this.
            ResultValueOkTaskAsync(okFunc).
            ToResultCollectionTaskAsync();
    }
}