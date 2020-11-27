using System.Collections.Generic;
using System.Threading.Tasks;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection
{
    /// <summary>
    /// Методы расширения для результирующего ответа с коллекцией
    /// </summary>
    public static class ResultCollectionTaskAsyncExtensions
    {
       /// <summary>
       /// Преобразовать в ответ со значением-коллекцией
       /// </summary>
        public static async Task<IResultValue<IReadOnlyCollection<TValue>>> ToResultValue<TValue>(this Task<IResultCollection<TValue>> @this) =>
            await @this.MapTaskAsync(awaitedThis => awaitedThis.ToResultValue());

        /// <summary>
        /// Преобразовать в результирующий ответ
        /// </summary>      
        public static async Task<IResultError> ToResultErrorTaskAsync<TValue>(this Task<IResultCollection<TValue>> @this) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis);
    }
}