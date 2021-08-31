﻿using System;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Errors;
using Functional.Models.Interfaces.Errors.Base;
using Functional.Models.Interfaces.Results;

namespace Functional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Преобразование значения в результирующий ответ с условием для задачи-объекта
    /// </summary>
    public static class ToResultValueWhereTaskAsyncExtensions
    {
        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием
        /// </summary>
        public static async Task<IResultValue<TValue>> ToResultValueWhereTaskAsync<TValue>(this Task<TValue> @this,
                                                                                           Func<TValue, bool> predicate,
                                                                                           Func<TValue, IErrorResult> badFunc)
            where TValue : notnull =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ToResultValueWhere(predicate, badFunc));

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        public static async Task<IResultValue<TValue>> ToResultValueWhereNullTaskAsync<TValue>(this Task<TValue?> @this,
                                                                                      Func<TValue, bool> predicate,
                                                                                      Func<TValue?, IErrorResult> badFunc)
            where TValue : class =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ToResultValueWhereNull(predicate, badFunc));

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        public static async Task<IResultValue<TValue>> ToResultValueWhereNullTaskAsync<TValue>(this Task<TValue?> @this,
                                                                                      Func<TValue, bool> predicate,
                                                                                      Func<TValue?, IErrorResult> badFunc)
            where TValue : struct =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ToResultValueWhereNull(predicate, badFunc));

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        public static async Task<IResultValue<TValueOut>> ToResultValueWhereNullOkBadTaskAsync<TValueIn, TValueOut>(this Task<TValueIn?> @this,
                                                                                      Func<TValueIn, bool> predicate,
                                                                                      Func<TValueIn, TValueOut> okFunc,
                                                                                      Func<TValueIn?, IErrorResult> badFunc)
            where TValueIn : class
            where TValueOut : notnull =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ToResultValueWhereNullOkBad(predicate, okFunc, badFunc));

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        public static async Task<IResultValue<TValueOut>> ToResultValueWhereNullOkBadTaskAsync<TValueIn, TValueOut>(this Task<TValueIn?> @this,
                                                                                      Func<TValueIn, bool> predicate,
                                                                                      Func<TValueIn, TValueOut> okFunc,
                                                                                      Func<TValueIn?, IErrorResult> badFunc)
            where TValueIn : struct
            where TValueOut : notnull =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ToResultValueWhereNullOkBad(predicate, okFunc, badFunc));
    }
}