using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection
{
    /// <summary>
    /// Асинхронное действие над внутренним типом результирующего ответа с коллекцией задачей-объектом
    /// </summary>
    public static class ResultCollectionVoidBindAsyncExtensions
    {
        /// <summary>
        /// Выполнить действие при положительном значении, вернуть результирующий ответ задачи-объекта
        /// </summary>      
        public static async Task<IResultCollection<TValue>> ResultCollectionVoidOkBindAsync<TValue>(this Task<IResultCollection<TValue>> @this,
                                                                                     Func<IReadOnlyCollection<TValue>, Task> action) =>
            await @this.
            VoidOkBindAsync(awaitedThis => awaitedThis.OkStatus,
                action: awaitedThis => action.Invoke(awaitedThis.Value));

        /// <summary>
        /// Выполнить действие при отрицательном значении, вернуть результирующий ответ задачи-объекта
        /// </summary>      
        public static async Task<IResultCollection<TValue>> ResultCollectionVoidBadBindAsync<TValue>(this Task<IResultCollection<TValue>> @this,
                                                                                      Func<IReadOnlyCollection<IErrorResult>, Task> action) =>
            await @this.
            VoidOkBindAsync(awaitedThis => awaitedThis.HasErrors,
                action: awaitedThis => action.Invoke(awaitedThis.Errors));

        /// <summary>
        /// Выполнить действие при положительном значении и выполнении условия вернуть результирующий ответ задачи-объекта
        /// </summary>    
        public static async Task<IResultCollection<TValue>> ResultCollectionVoidOkWhereBindAsync<TValue>(this Task<IResultCollection<TValue>> @this,
                                                                                          Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                                                          Func<IReadOnlyCollection<TValue>, Task> action) =>
            await @this.
            VoidOkBindAsync(awaitedThis => awaitedThis.OkStatus && predicate(awaitedThis.Value),
                action: awaitedThis => action.Invoke(awaitedThis.Value));
    }
}