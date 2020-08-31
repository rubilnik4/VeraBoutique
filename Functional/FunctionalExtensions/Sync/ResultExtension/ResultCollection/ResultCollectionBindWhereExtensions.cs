using System;
using System.Collections.Generic;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection
{
    /// <summary>
    /// Обработка условий для результирующего связывающего ответа с коллекцией
    /// </summary>
    public static class ResultCollectionBindWhereExtensions
    {
        /// <summary>
        /// Выполнение положительного условия результирующего ответа со связыванием или возвращение предыдущей ошибки в результирующем ответе с коллекцией
        /// </summary>   
        public static IResultCollection<TValueOut> ResultCollectionBindOk<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                               Func<IReadOnlyCollection<TValueIn>, IResultCollection<TValueOut>> okFunc) =>
            @this.OkStatus
                ? okFunc.Invoke(@this.Value)
                : new ResultCollection<TValueOut>(@this.Errors);

        /// <summary>
        /// Выполнение негативного условия результирующего ответа или возвращение положительного в результирующем ответе с коллекцией
        /// </summary>   
        public static IResultCollection<TValue> ResultCollectionBindBad<TValue>(this IResultCollection<TValue> @this,
                                                                                Func<IReadOnlyCollection<IErrorResult>, IResultCollection<TValue>> badFunc) =>
            @this.OkStatus
                ? @this
                : badFunc.Invoke(@this.Errors);
    }
}