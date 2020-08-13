using System;
using System.Collections.Generic;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.ResultExtension
{
    /// <summary>
    /// Обработка условий для результирующего ответа
    /// </summary>
    public static class ResultErrorWhereExtensions
    {
        /// <summary>
        /// Выполнение положительного или негативного условия в результирующем ответе
        /// </summary>      
        public static TValueOut ResultOkBad<TValueOut>(this IResultError @this,
                                                       Func<TValueOut> okFunc,
                                                       Func<IReadOnlyList<IErrorResult>, TValueOut> badFunc) =>
            @this.OkStatus
                ? okFunc.Invoke()
                : badFunc.Invoke(@this.Errors);
    }
}