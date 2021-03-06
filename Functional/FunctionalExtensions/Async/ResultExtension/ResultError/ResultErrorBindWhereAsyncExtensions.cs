﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Async.ResultExtension.ResultError
{
    /// <summary>
    /// Обработка условий для асинхронного результирующего связывающего ответа
    /// </summary>
    public static class ResultErrorBindWhereAsyncExtensions
    {
        /// <summary>
        /// Выполнение положительного или негативного условия результирующего ответа со связыванием или возвращение предыдущей ошибки в результирующем ответе
        /// </summary>   
        public static async Task<IResultError> ResultErrorBindOkBadAsync(this IResultError @this,
                                                             Func<Task<IResultError>> okFunc,
                                                             Func<IReadOnlyCollection<IErrorResult>, Task<IResultError>> badFunc) =>
            @this.OkStatus
                ? await okFunc.Invoke()
                : await badFunc.Invoke(@this.Errors);

        /// <summary>
        /// Выполнение положительного условия асинхронного результирующего ответа со связыванием или возвращение предыдущей ошибки в результирующем ответе
        /// </summary>   
        public static async Task<IResultError> ResultErrorBindOkAsync(this IResultError @this, Func<Task<IResultError>> okFunc) =>
            @this.OkStatus
                ? await okFunc.Invoke()
                : @this;
    }
}