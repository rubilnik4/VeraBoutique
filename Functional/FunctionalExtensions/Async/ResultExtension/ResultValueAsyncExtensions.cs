using System.Collections.Generic;
using System.Threading.Tasks;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Async.ResultExtension
{
    /// <summary>
    /// Асинхронные методы расширения для результирующего ответа со значением
    /// </summary>
    public static class ResultValueAsyncExtensions
    {
        /// <summary>
        /// Преобразовать объект-задачу в результирующий ответ со значением
        /// </summary>      
        public static async Task<IResultCollection<TValue>> ToResultCollectionTaskAsync<TValue>(this Task<IResultValue<IEnumerable<TValue>>> @this) =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.OkStatus
                                        ? new ResultCollection<TValue>(thisAwaited.Value)
                                        : new ResultCollection<TValue>(thisAwaited.Errors));
    }
}