﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Async.ResultExtension.ResultValue
{
    /// <summary>
    /// Асинхронное действие над внутренним типом результирующего ответа со значением
    /// </summary>
    public static class ResultValueVoidAsyncExtensions
    {
        /// <summary>
        /// Выполнить действие при положительном значении, вернуть результирующий ответ
        /// </summary>      
        public static async Task<IResultValue<TValue>> ResultValueVoidOkAsync<TValue>(this IResultValue<TValue> @this,
                                                                                 Func<TValue, Task> action) =>
            await @this.
            VoidOkAsync(_ => @this.OkStatus,
                action: _ => action.Invoke(@this.Value));

        /// <summary>
        /// Выполнить действие при отрицательном значении, вернуть результирующий ответ
        /// </summary>      
        public static async Task<IResultValue<TValue>> ResultValueVoidBadAsync<TValue>(this IResultValue<TValue> @this,
                                                                                  Func<IReadOnlyCollection<IErrorResult>, Task> action) =>
            await @this.
            VoidOkAsync(_ => @this.HasErrors,
                action: _ => action.Invoke(@this.Errors));

        /// <summary>
        /// Выполнить действие при положительном значении и выполнении условия вернуть результирующий ответ
        /// </summary>    
        public static async Task<IResultValue<TValue>> ResultValueVoidOkWhereAsync<TValue>(this IResultValue<TValue> @this,
                                                                          Func<TValue, bool> predicate,
                                                                          Func<TValue, Task> action) =>
            await  @this.
            VoidOkAsync(_ => @this.OkStatus && predicate(@this.Value),
                action: _ => action.Invoke(@this.Value));
    }
}