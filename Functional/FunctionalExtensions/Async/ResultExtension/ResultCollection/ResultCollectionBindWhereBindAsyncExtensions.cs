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
    /// Обработка условий для асинхронного результирующего асинхронного связывающего ответа с коллекцией для задачи-объекта
    /// </summary>
    public static class ResultCollectionBindWhereBindAsyncExtensions
    {
        /// <summary>
        /// Выполнение положительного условия асинхронного результирующего ответа или возвращение предыдущей ошибки в результирующем ответе с коллекцией для задачи-объекта
        /// </summary>   
        public static async Task<IResultCollection<TValueOut>> ResultCollectionBindOkBindAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                      Func<IReadOnlyCollection<TValueIn>, Task<IResultCollection<TValueOut>>> okFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultCollectionBindOkAsync(okFunc));

        /// <summary>
        /// Выполнение негативного условия асинхронного результирующего ответа или возвращение положительного в результирующем ответе с коллекцией для задачи-объекта
        /// </summary>   
        public static async Task<IResultCollection<TValue>> ResultCollectionBindBadBindAsync<TValue>(this Task<IResultCollection<TValue>> @this,
                                                                                       Func<IReadOnlyCollection<IErrorResult>, Task<IResultCollection<TValue>>> badFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultCollectionBindBadAsync(badFunc));

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа или вернуть результат с ошибками для ответа с коллекцией для задачи-объекта
        /// </summary>
        public static async Task<IResultCollection<TValue>> ResultCollectionBindErrorsOkBindAsync<TValue>(this Task<IResultCollection<TValue>> @this,
                                                                                       Func<IReadOnlyCollection<TValue>, Task<IResultError>> okFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultCollectionBindErrorsOkAsync(okFunc));


    }
}