using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.Models.Interfaces.Result;
using static Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection.ResultCollectionTryExtensions;

namespace Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection
{
    /// <summary>
    /// Методы расширения для результирующего ответа с коллекцией и обработкой исключений для задачи-объекта
    /// </summary>
    public static class ResultCollectionTryTaskAsyncExtensions
    {
        /// <summary>
        /// Связать результирующий ответ с коллекцией с обработкой функции при положительном условии для задачи-объекта
        /// </summary>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionTryOkTaskAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, IEnumerable<TValueOut>> func, 
                                                                                                            IErrorResult error) =>
            await @this.
            ResultCollectionBindOkTaskAsync(value => ResultCollectionTry(() => func.Invoke(value), error));
    }
}