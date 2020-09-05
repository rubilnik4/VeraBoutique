using System;
using System.Threading.Tasks;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Async.ResultExtension.ResultError
{
    /// <summary>
    /// Обработка условий для асинхронного результирующего связывающего ответа для задачи-объекта
    /// </summary>
    public static class ResultErrorBindWhereBindAsyncExtensions
    {
        /// <summary>
        /// Выполнение положительного условия асинхронного результирующего ответа со связыванием или возвращение предыдущей ошибки в результирующем ответе
        /// </summary>   
        public static async Task<IResultError> ResultErrorBindOkBindAsync(this Task<IResultError> @this, Func<Task<IResultError>> okFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultErrorBindOkAsync(okFunc));
    }
}