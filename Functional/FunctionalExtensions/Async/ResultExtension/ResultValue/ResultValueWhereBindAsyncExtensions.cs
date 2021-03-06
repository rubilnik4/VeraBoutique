﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Async.ResultExtension.ResultValue
{
    /// <summary>
    /// Обработка асинхронных условий для результирующего ответа со значением задачей-объектом
    /// </summary>
    public static class ResultValueWhereTaskBindAsyncExtensions
    {
        /// <summary>
        /// Выполнение условия или возвращение предыдущей ошибки в асинхронном результирующем ответе задачи-объекта
        /// </summary>      
        public static async Task<IResultValue<TValueOut>> ResultValueContinueBindAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                            Func<TValueIn, bool> predicate,
                                                                                                            Func<TValueIn, Task<TValueOut>> okFunc,
                                                                                                            Func<TValueIn, Task<IEnumerable<IErrorResult>>> badFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueContinueAsync(predicate, okFunc, badFunc));

        /// <summary>
        /// Выполнение положительного или негативного условия в асинхронном результирующем ответе задачи-объекта
        /// </summary>      
        public static async Task<IResultValue<TValueOut>> ResultValueOkBadBindAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                         Func<TValueIn, Task<TValueOut>> okFunc,
                                                                                                         Func<IReadOnlyCollection<IErrorResult>, Task<TValueOut>> badFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueOkBadAsync(okFunc, badFunc));

        /// <summary>
        /// Выполнение положительного условия или возвращение предыдущей ошибки в асинхронном результирующем ответе задачи-объекта
        /// </summary>   
        public static async Task<IResultValue<TValueOut>> ResultValueOkBindAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                      Func<TValueIn, Task<TValueOut>> okFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueOkAsync(okFunc));

        /// <summary>
        /// Выполнение негативного условия или возвращение положительного условия в асинхронном результирующем ответе задачи-объекта
        /// </summary>   
        public static async Task<IResultValue<TValue>> ResultValueBadBindAsync<TValue>(this Task<IResultValue<TValue>> @this,
                                                                                       Func<IReadOnlyCollection<IErrorResult>, Task<TValue>> badFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueBadAsync(badFunc));
    }
}