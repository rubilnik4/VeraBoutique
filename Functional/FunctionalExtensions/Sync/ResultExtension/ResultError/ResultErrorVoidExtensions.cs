﻿using System;
using System.Collections.Generic;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Sync.ResultExtension.ResultError
{
    /// <summary>
    /// Действие над внутренним типом результирующего ответа
    /// </summary>
    public static class ResultErrorVoidExtensions
    {
        /// <summary>
        /// Выполнить действие при положительном значении, вернуть результирующий ответ
        /// </summary>      
        public static IResultError ResultErrorVoidOk(this IResultError @this, Action action) =>
            @this.
            VoidOk(_ => @this.OkStatus,
                action: _ => action.Invoke());

        /// <summary>
        /// Выполнить действие при отрицательном значении, вернуть результирующий ответ
        /// </summary>      
        public static IResultError ResultErrorVoidBad(this IResultError @this,
                                                      Action<IReadOnlyCollection<IErrorResult>> actionBad) =>
            @this.
            VoidOk(_ => @this.HasErrors,
                action: _ => actionBad.Invoke(@this.Errors));

        /// <summary>
        /// Выполнить действие, вернуть результирующий ответ
        /// </summary>      
        public static IResultError ResultErrorVoidOkBad(this IResultError @this,
                                                                Action actionOk,
                                                                Action<IReadOnlyCollection<IErrorResult>> actionBad) =>
            @this.
            VoidWhere(_ => @this.OkStatus,
                actionOk: _ => actionOk.Invoke(),
                actionBad: _ => actionBad.Invoke(@this.Errors));

        /// <summary>
        /// Выполнить действие при положительном значении и выполнении условия вернуть результирующий ответ
        /// </summary>    
        public static IResultError ResultErrorVoidOkWhere(this IResultError @this,
                                                          Func<bool> predicate,
                                                          Action action) =>
            @this.
            VoidOk(_ => @this.OkStatus && predicate(),
                action: _ => action.Invoke());
    }
}