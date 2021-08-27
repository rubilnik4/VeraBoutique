﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Sync.ResultExtension;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Interfaces.Errors;
using Functional.Models.Interfaces.Errors.Base;
using Functional.Models.Interfaces.Results;

namespace Functional.FunctionalExtensions.Async.ResultExtension.ResultValues
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
        public static async Task<IResultValue<TValue>> ToResultValueNullValueCheckTaskAsync<TValue>(this Task<TValue> @this, IErrorResult errorNull)
            where TValue : notnull =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ToResultValueNullValueCheck(errorNull));

        /// <summary>
        /// Преобразовать значение в результирующий ответ с проверкой на нуль для задачи-объекта
        /// </summary>
        public static async Task<IResultValue<TValue>> ToResultValueNullCheckTaskAsync<TValue>(this Task<TValue?> @this, IErrorResult errorNull) 
            where TValue : class =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ToResultValueNullCheck(errorNull));

        /// <summary>
        /// Преобразовать значение в результирующий ответ с проверкой на нуль для задачи-объекта
        /// </summary>
        public static async Task<IResultValue<TValue>> ToResultValueNullCheckTaskAsync<TValue>(this Task<TValue?> @this, IErrorResult errorNull)
            where TValue : struct =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ToResultValueNullCheck(errorNull));
    }
}