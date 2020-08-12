using System;
using System.Collections.Generic;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.ResultExtension
{
    /// <summary>
    /// Действие над внутренним типом результирующего ответа со значением
    /// </summary>
    public static class ResultValueVoidExtensions
    {
        /// <summary>
        /// Выполнить действие, вернуть результирующий ответ
        /// </summary>      
        public static IResultValue<TValue> ResultVoid<TValue>(this IResultValue<TValue> @this, Action<TValue> action)
        {
            action.Invoke(@this.Value);
            return @this;
        }

        /// <summary>
        /// Выполнить действие при положительном значении, вернуть результирующий ответ
        /// </summary>      
        public static IResultValue<TValue> ResultVoidOk<TValue>(this IResultValue<TValue> @this, Action<TValue> action)
        {
            if (@this.OkStatus) action.Invoke(@this.Value);
            return @this;
        }

        /// <summary>
        /// Выполнить действие при отрицательном значении, вернуть результирующий ответ
        /// </summary>      
        public static IResultValue<TValue> ResultVoidBad<TValue>(this IResultValue<TValue> @this, Action<IReadOnlyList<IErrorResult>> action)
        {
            if (@this.HasErrors) action.Invoke(@this.Errors);
            return @this;
        }
    }
}