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
        /// Преобразовать в результирующий ответ со значением
        /// </summary>      
        public static IResultValue<TValueOut> ToResultValue<TValueOut>(this IResultError @this,
                                                                       Func<TValueOut> okFunc) =>
            @this.OkStatus
                ? new ResultValue<TValueOut>(okFunc.Invoke())
                : new ResultValue<TValueOut>(@this.Errors);
    }
}