using System;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Results;

namespace Functional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Преобразование значения в результирующий ответ с условием асинхронно
    /// </summary>
    public static class ToResultValueWhereAsyncExtensions
    {
        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием
        /// </summary>
        public static async Task<IResultValue<TValue>> ToResultValueWhereAsync<TValue>(this TValue @this,
                                                                                           Func<TValue, bool> predicate,
                                                                                           Func<TValue, Task<IErrorResult>> badFunc)
            where TValue : notnull =>
          await @this.WhereContinueAsync(predicate,
                              value => Task.FromResult(new ResultValue<TValue>(value)),
                              value => badFunc(value).
                                       MapTaskAsync(error => new ResultValue<TValue>(error)));
    }
}