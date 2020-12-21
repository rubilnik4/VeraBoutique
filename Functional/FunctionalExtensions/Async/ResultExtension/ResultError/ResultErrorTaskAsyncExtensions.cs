using System.Threading.Tasks;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultError;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Async.ResultExtension.ResultError
{
    /// <summary>
    /// Методы расширения для результирующего ответа задачи-объекта
    /// </summary>
    public static class ResultErrorTaskAsyncExtensions
    {
        /// <summary>
        /// Преобразовать в результирующий ответ со значением результирующего ответа
        /// </summary>      
        public static async Task<IResultValue<TValue>> ToResultBindValueTaskAsync<TValue>(this Task<IResultError> @this,
                                                                                          IResultValue<TValue> resultValue) =>
            await @this.
            MapTaskAsync(result => result.ToResultBindValue(resultValue));
    }
}