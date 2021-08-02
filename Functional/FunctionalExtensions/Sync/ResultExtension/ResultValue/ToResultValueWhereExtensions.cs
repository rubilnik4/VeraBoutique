using System;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue
{
    /// <summary>
    /// Преобразование значения в результирующий ответ с условием
    /// </summary>
    public static class ToResultValueWhereTaskASyncExtensions
    {
        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием
        /// </summary>
        public static IResultValue<TValue> ToResultValueWhere<TValue>(this TValue @this,
                                                                      Func<TValue, bool> predicate,
                                                                      Func<TValue, IErrorResult> badFunc)
            where TValue : notnull =>
            @this.WhereContinue(predicate,
                                value => (IResultValue<TValue>)new ResultValue<TValue>(value),
                                value => new ResultValue<TValue>(badFunc(value)));

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием
        /// </summary>
        public static IResultValue<TValueOut> ToResultValueWhereOkBad<TValueIn, TValueOut>(this TValueIn @this,
                                                                                      Func<TValueIn, bool> predicate,
                                                                                      Func<TValueIn, TValueOut> okFunc,
                                                                                      Func<TValueIn, IErrorResult> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
          @this.WhereContinue(predicate,
                              value => new ResultValue<TValueOut>(okFunc(value)),
                              value => new ResultValue<TValueOut>(badFunc(value)));
    }
}