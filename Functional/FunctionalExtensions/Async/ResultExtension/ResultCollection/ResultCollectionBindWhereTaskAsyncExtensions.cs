using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection
{
    /// <summary>
    /// Обработка условий для результирующего асинхронного связывающего ответа с коллекцией для задачи-объекта
    /// </summary>
    public static class ResultCollectionBindWhereTaskAsyncExtensions
    {
        /// <summary>
        /// Выполнение положительного условия результирующего ответа или возвращение предыдущей ошибки в результирующем ответе с коллекцией для задачи-объекта
        /// </summary>   
        public static async Task<IResultCollection<TValueOut>> ResultCollectionBindOkTaskAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                      Func<IReadOnlyCollection<TValueIn>, IResultCollection<TValueOut>> okFunc) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultCollectionBindOk(okFunc));

        /// <summary>
        /// Выполнение негативного условия результирующего ответа или возвращение положительного в результирующем ответе с коллекцией для задачи-объекта
        /// </summary>   
        public static async Task<IResultCollection<TValue>> ResultCollectionBindBadTaskAsync<TValue>(this Task<IResultCollection<TValue>> @this,
                                                                                       Func<IReadOnlyCollection<IErrorResult>, IResultCollection<TValue>> badFunc) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultCollectionBindBad(badFunc));

        /// <summary>
        /// Добавить ошибки результирующего ответа или вернуть результат с ошибками для ответа с коллекцией для задачи-объекта
        /// </summary>
        public static async Task<IResultCollection<TValue>> ResultCollectionBindErrorsOkTaskAsync<TValue>(this Task<IResultCollection<TValue>> @this,
                                                                                       Func<IReadOnlyCollection<TValue>, IResultError> okFunc) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultCollectionBindErrorsOk(okFunc));


    }
}