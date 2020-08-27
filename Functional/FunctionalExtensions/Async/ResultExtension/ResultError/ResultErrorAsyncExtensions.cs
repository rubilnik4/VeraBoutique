using System.Threading.Tasks;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Async.ResultExtension.ResultError
{
    /// <summary>
    /// Асинхронные методы расширения для результирующего ответа
    /// </summary>
    public static class ResultErrorAsyncExtensions
    {
        /// <summary>
        /// Преобразовать объект-задачу в результирующий ответ со значением
        /// </summary>      
        public static async Task<IResultValue<TValue>> ToResultValueTaskAsync<TValue>(this Task<IResultError> @this, TValue value) =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.OkStatus
                                        ? new ResultValue<TValue>(value)
                                        : new ResultValue<TValue>(thisAwaited.Errors));
    }
}