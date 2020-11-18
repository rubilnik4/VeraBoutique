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
            okFunc: value => (IResultValue<TValue>)new ResultValue<TValue>(value),
            badFunc: value => new ResultValue<TValue>(badFunc(value)));
    }
}