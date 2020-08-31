using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Async.ResultExtension.ResultValue
{
    /// <summary>
    /// Обработка условий для результирующего асинхронного связывающего ответа со значением
    /// </summary>
    public static class ResultValueBindWhereAsyncExtensions
    {
        /// <summary>
        /// Выполнение положительного условия результирующего асинхронного ответа со связыванием или возвращение предыдущей ошибки в результирующем ответе
        /// </summary>   
        public static async Task<IResultValue<TValueOut>> ResultValueBindOkAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                                      Func<TValueIn, Task<IResultValue<TValueOut>>> okFunc) =>
            @this.OkStatus
                ? await okFunc.Invoke(@this.Value)
                : new ResultValue<TValueOut>(@this.Errors);

        /// <summary>
        /// Выполнение негативного условия результирующего асинхронного ответа или возвращение положительного в результирующем ответе
        /// </summary>   
        public static async Task<IResultValue<TValue>> ResultValueBindBadAsync<TValue>(this IResultValue<TValue> @this,
                                                                                       Func<IReadOnlyCollection<IErrorResult>, Task<IResultValue<TValue>>> badFunc) =>
            @this.OkStatus
                ? @this
                : await badFunc.Invoke(@this.Errors);
    }
}