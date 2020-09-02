﻿using System;
using System.Collections.Generic;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue
{
    /// <summary>
    /// Действие над внутренним типом результирующего ответа со значением
    /// </summary>
    public static class ResultValueVoidExtensions
    {
        /// <summary>
        /// Выполнить действие при положительном значении, вернуть результирующий ответ
        /// </summary>      
        public static IResultValue<TValue> ResultValueVoidOk<TValue>(this IResultValue<TValue> @this, Action<TValue> action) =>
            @this.
            VoidOk(_ => @this.OkStatus,
                action: _ => action.Invoke(@this.Value));

        /// <summary>
        /// Выполнить действие при отрицательном значении, вернуть результирующий ответ
        /// </summary>      
        public static IResultValue<TValue> ResultValueVoidBad<TValue>(this IResultValue<TValue> @this,
                                                                 Action<IReadOnlyCollection<IErrorResult>> action) =>
            @this.
            VoidOk(_ => @this.HasErrors,
                action: _ => action.Invoke(@this.Errors));

        /// <summary>
        /// Выполнить действие при положительном значении и выполнении условия вернуть результирующий ответ
        /// </summary>    
        public static IResultValue<TValue> ResultValueVoidOkWhere<TValue>(this IResultValue<TValue> @this,
                                                                     Func<TValue, bool> predicate,
                                                                     Action<TValue> action) =>
            @this.
            VoidOk(_ => @this.OkStatus && predicate(@this.Value),
                action: _ => action.Invoke(@this.Value));
    }
}