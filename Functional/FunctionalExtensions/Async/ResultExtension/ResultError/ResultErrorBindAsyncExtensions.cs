using System.Threading.Tasks;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Async.ResultExtension.ResultError
{
    /// <summary>
    /// Асинхронные методы расширения для результирующего ответа задачи-объекта
    /// </summary>
    public static class ResultErrorBindAsyncExtensions
    {
        /// <summary>
        /// Преобразовать в результирующий ответ со значением результирующего ответа асинхронно
        /// </summary>      
        public static async Task<IResultValue<TValue>> ToResultBindValueBindAsync<TValue>(this Task<IResultError> @this, Task<IResultValue<TValue>> resultValue) =>
            await @this.
            MapBindAsync(result => result.ToResultBindValueAsync(resultValue));
    }
}