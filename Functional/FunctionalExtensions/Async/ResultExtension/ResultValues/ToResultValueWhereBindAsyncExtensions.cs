using System;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Interfaces.Errors;
using Functional.Models.Interfaces.Errors.Base;
using Functional.Models.Interfaces.Results;

namespace Functional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Преобразование значения в результирующий ответ с условием асинхронно для задачи-объекта
    /// </summary>
    public static class ToResultValueWhereBindAsyncExtensions
    {
        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием
        /// </summary>
        public static async Task<IResultValue<TValue>> ToResultValueWhereBindAsync<TValue>(this Task<TValue> @this,
                                                                                           Func<TValue, bool> predicate,
                                                                                           Func<TValue, Task<IErrorResult>> badFunc)
            where TValue : notnull =>
          await @this.
          MapBindAsync(thisAwaited => thisAwaited.ToResultValueWhereAsync(predicate, badFunc));
    }
}