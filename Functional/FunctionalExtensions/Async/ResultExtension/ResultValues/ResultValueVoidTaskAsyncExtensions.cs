﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Interfaces.Errors;
using Functional.Models.Interfaces.Errors.Base;
using Functional.Models.Interfaces.Results;

namespace Functional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Асинхронное действие над внутренним типом результирующего ответа со значением задачей-объектом
    /// </summary>
    public static class ResultCollectionVoidTaskAsyncExtensions
    {
        /// <summary>
        /// Выполнить действие при положительном значении, вернуть результирующий ответ задачи-объекта
        /// </summary>      
        public static async Task<IResultValue<TValue>> ResultValueVoidOkTaskAsync<TValue>(this Task<IResultValue<TValue>> @this,
                                                                                          Action<TValue> action) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueVoidOk(action));

        /// <summary>
        /// Выполнить действие при отрицательном значении, вернуть результирующий ответ задачи-объекта
        /// </summary>      
        public static async Task<IResultValue<TValue>> ResultValueVoidBadTaskAsync<TValue>(this Task<IResultValue<TValue>> @this,
                                                                                      Action<IReadOnlyCollection<IErrorResult>> action) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueVoidBad(action));

        /// <summary>
        /// Выполнить действие, вернуть результирующий ответ задачи-объекта
        /// </summary>      
        public static async Task<IResultValue<TValue>> ResultValueVoidOkBadTaskAsync<TValue>(this Task<IResultValue<TValue>> @this,
                                                                                           Action<TValue> actionOk,
                                                                                           Action<IReadOnlyCollection<IErrorResult>> actionBad) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueVoidOkBad(actionOk, actionBad));

        /// <summary>
        /// Выполнить действие при положительном значении и выполнении условия вернуть результирующий ответ задачи-объекта
        /// </summary>    
        public static async Task<IResultValue<TValue>> ResultValueVoidOkWhereTaskAsync<TValue>(this Task<IResultValue<TValue>> @this,
                                                                                          Func<TValue, bool> predicate,
                                                                                          Action<TValue> action) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueVoidOkWhere(predicate, action));
    }
}