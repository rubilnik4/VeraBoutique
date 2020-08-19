using System;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Sync.ResultExtension
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
    }
}