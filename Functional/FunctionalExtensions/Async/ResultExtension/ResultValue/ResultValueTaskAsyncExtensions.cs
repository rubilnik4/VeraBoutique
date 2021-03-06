﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Sync.ResultExtension;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Async.ResultExtension.ResultValue
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

        /// <summary>
        /// Преобразовать в результирующий ответ со значением в коллекцию задачи-объекта
        /// </summary>      
        public static async Task<IResultCollection<TValue>> ToResultCollectionTaskAsync<TValue>(this Task<IResultValue<List<TValue>>> @this) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ToResultCollection());

        /// <summary>
        /// Преобразовать в результирующий ответ
        /// </summary>      
        public static async Task<IResultError> ToResultErrorTaskAsync<TValue>(this Task<IResultValue<TValue>> @this) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis);

        /// <summary>
        /// Преобразовать значение в результирующий ответ с проверкой на нуль для задачи-объекта
        /// </summary>
        public static async Task<IResultValue<TValue>> ToResultValueNullCheckTaskAsync<TValue>(this Task<TValue?> @this, IErrorResult errorNull) 
            where TValue : class =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ToResultValueNullCheck(errorNull));
    }
}