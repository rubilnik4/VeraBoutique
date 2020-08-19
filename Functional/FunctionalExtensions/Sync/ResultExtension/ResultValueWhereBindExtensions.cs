using System;
using System.Collections.Generic;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Sync.ResultExtension
{
    /// <summary>
    /// Обработка условий для результирующего связывающего ответа со значением
    /// </summary>
    public static class ResultValueWhereBindExtensions
    {
        /// <summary>
        /// Выполнение положительного условия результирующего ответа со связыванием или возвращение предыдущей ошибки в результирующем ответе
        /// </summary>   
        public static IResultValue<TValueOut> ResultValueOkBind<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                     Func<TValueIn, IResultValue<TValueOut>> okFunc) =>
            @this.OkStatus
                ? okFunc.Invoke(@this.Value)
                : new ResultValue<TValueOut>(@this.Errors);

        /// <summary>
        /// Выполнение негативного условия результирующего ответа или возвращение положительного в результирующем ответе
        /// </summary>   
        public static IResultValue<TValue> ResultValueBadBind<TValue>(this IResultValue<TValue> @this,
                                                                      Func<IReadOnlyList<IErrorResult>, IResultValue<TValue>> badFunc) =>
            @this.OkStatus
                ? @this
                : badFunc.Invoke(@this.Errors);
    }
}