﻿using System.Collections.Generic;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Sync.ResultExtension.ResultError
{
    /// <summary>
    /// Методы расширения для результирующего ответа
    /// </summary>
    public static class ResultErrorExtensions
    {
        /// <summary>
        /// Преобразовать в результирующий ответ со значением
        /// </summary>      
        public static IResultValue<TValue> ToResultValue<TValue>(this IResultError @this, TValue value) =>
            @this.OkStatus
                ? new ResultValue<TValue>(value)
                : new ResultValue<TValue>(@this.Errors);

        /// <summary>
        /// Преобразовать в результирующий ответ со значением результирующего ответа
        /// </summary>      
        public static IResultValue<TValue> ToResultBindValue<TValue>(this IResultError @this, IResultValue<TValue> resultValue) =>
            @this.OkStatus
                ? resultValue
                : new ResultValue<TValue>(@this.Errors);

        /// <summary>
        /// Преобразовать в результирующий ответ со значением
        /// </summary>      
        public static IResultCollection<TValue> ToResultCollection<TValue>(this IResultError @this, IReadOnlyCollection<TValue> value) =>
            @this.OkStatus
                ? new ResultCollection<TValue>(value)
                : new ResultCollection<TValue>(@this.Errors);
    }
}