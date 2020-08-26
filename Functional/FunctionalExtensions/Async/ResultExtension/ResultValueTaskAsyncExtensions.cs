using System.Collections.Generic;
using System.Collections.ObjectModel;
using Functional.Models.Interfaces.Result;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Sync.ResultExtension;

namespace Functional.FunctionalExtensions.Async.ResultExtension
{
    /// <summary>
    /// Методы расширения для результирующего ответа задачи-объекта
    /// </summary>
    public static class ResultValueTaskAsyncExtensions
    {
        /// <summary>
        /// Преобразовать в результирующий ответ со значением в коллекцию задачи-объекта
        /// </summary>      
        public static async Task<IResultCollection<TValue>> ToResultCollectionTaskAsync<TValue>(this Task<IResultValue<IEnumerable<TValue>>> @this) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ToResultCollection());

        /// <summary>
        /// Преобразовать в результирующий ответ со значением в коллекцию задачи-объекта
        /// </summary>      
        public static async Task<IResultCollection<TValue>> ToResultCollectionTaskAsync<TValue>(this Task<IResultValue<IReadOnlyCollection<TValue>>> @this) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ToResultCollection());

        /// <summary>
        /// Преобразовать в результирующий ответ со значением в коллекцию задачи-объекта
        /// </summary>      
        public static async Task<IResultCollection<TValue>> ToResultCollectionTaskAsync<TValue>(this Task<IResultValue<ReadOnlyCollection<TValue>>> @this) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ToResultCollection());
    }
}