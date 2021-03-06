﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Async.ResultExtension.ResultValue
{
    /// <summary>
    /// Обработка условий для асинхронного результирующего связывающего ответа со значением для задачи-объекта
    /// </summary>
    public static class ResultValueBindWhereBindAsyncExtensions
    {
        /// <summary>
        /// Выполнение условия или возвращение предыдущей ошибки в результирующем ответе со значением задачи-объекта
        /// </summary>      
        public static async Task<IResultValue<TValueOut>> ResultValueBindContinueBindAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                            Func<TValueIn, bool> predicate,
                                                                                                            Func<TValueIn, Task<IResultValue<TValueOut>>> okFunc,
                                                                                                            Func<TValueIn, Task<IEnumerable<IErrorResult>>> badFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueBindContinueAsync(predicate, okFunc, badFunc));

        /// <summary>
        /// Выполнение условия или возвращение предыдущей ошибки в результирующем ответе со значение задачи-объекта
        /// </summary>      
        public static async Task<IResultValue<TValueOut>> ResultValueBindWhereBindAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                             Func<TValueIn, bool> predicate,
                                                                                                             Func<TValueIn, Task<IResultValue<TValueOut>>> okFunc,
                                                                                                             Func<TValueIn, Task<IResultValue<TValueOut>>> badFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueBindWhereAsync(predicate, okFunc, badFunc));

        /// <summary>
        /// Выполнение условия положительного или негативного условия в результирующем ответе со значение задачи-объекта
        /// </summary>      
        public static async Task<IResultValue<TValueOut>> ResultValueBindOkBadBindAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                             Func<TValueIn, Task<IResultValue<TValueOut>>> okFunc,
                                                                                                             Func<IReadOnlyCollection<IErrorResult>, Task<IResultValue<TValueOut>>> badFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueBindOkBadAsync(okFunc, badFunc));

        /// <summary>
        /// Выполнение асинхронного положительного условия результирующего ответа или возвращение предыдущей ошибки в результирующем ответе для задачи-объекта
        /// </summary>   
        public static async Task<IResultValue<TValueOut>> ResultValueBindOkBindAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                          Func<TValueIn, Task<IResultValue<TValueOut>>> okFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueBindOkAsync(okFunc));

        /// <summary>
        /// Выполнение асинхронного негативного условия результирующего ответа или возвращение положительного в результирующем ответе для задачи-объекта
        /// </summary>   
        public static async Task<IResultValue<TValue>> ResultValueBindBadBindAsync<TValue>(this Task<IResultValue<TValue>> @this,
                                                                                           Func<IReadOnlyCollection<IErrorResult>, Task<IResultValue<TValue>>> badFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueBindBadAsync(badFunc));

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа или вернуть результат с ошибками для ответа со значением для задачи-объекта
        /// </summary>
        public static async Task<IResultValue<TValue>> ResultValueBindErrorsOkBindAsync<TValue>(this Task<IResultValue<TValue>> @this,
                                                                                            Func<TValue, Task<IResultError>> okFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueBindErrorsOkAsync(okFunc));
    }
}