using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Async.ResultExtension
{
    /// <summary>
    /// Асинхронное действие над внутренним типом результирующего ответа со значением задачей-объектом
    /// </summary>
    public static class ResultValueVoidBindExtensions
    {
        /// <summary>
        /// Выполнить действие при положительном значении, вернуть результирующий ответ задачи-объекта
        /// </summary>      
        public static async Task<IResultValue<TValue>> ResultVoidOkBindAsync<TValue>(this Task<IResultValue<TValue>> @this,
                                                                                     Func<TValue, Task> action) =>
            await @this.
            VoidOkBindAsync(awaitedThis => awaitedThis.OkStatus,
                action: awaitedThis => action.Invoke(awaitedThis.Value));

        /// <summary>
        /// Выполнить действие при отрицательном значении, вернуть результирующий ответ задачи-объекта
        /// </summary>      
        public static async Task<IResultValue<TValue>> ResultVoidBadBindAsync<TValue>(this Task<IResultValue<TValue>> @this,
                                                                                      Func<IReadOnlyCollection<IErrorResult>, Task> action) =>
            await @this.
            VoidOkBindAsync(awaitedThis => awaitedThis.HasErrors,
                action: awaitedThis => action.Invoke(awaitedThis.Errors));

        /// <summary>
        /// Выполнить действие при положительном значении и выполнении условия вернуть результирующий ответ задачи-объекта
        /// </summary>    
        public static async Task<IResultValue<TValue>> ResultVoidOkWhereBindAsync<TValue>(this Task<IResultValue<TValue>> @this,
                                                                                          Func<TValue, bool> predicate,
                                                                                          Func<TValue, Task> action) =>
            await @this.
            VoidOkBindAsync(awaitedThis => awaitedThis.OkStatus && predicate(awaitedThis.Value),
                action: awaitedThis => action.Invoke(awaitedThis.Value));
    }
}