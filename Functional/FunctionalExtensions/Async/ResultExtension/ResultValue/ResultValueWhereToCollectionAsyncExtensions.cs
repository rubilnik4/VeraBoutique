﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Async.ResultExtension.ResultValue
{
    /// <summary>
    /// Обработка условий для асинхронного результирующего ответа с значением с возвращением к коллекции
    /// </summary>
    public static class ResultValueWhereToCollectionAsyncExtensions
    {
        /// <summary>
        /// Выполнение асинхронного условия или возвращение предыдущей ошибки в результирующем ответе значения с возвращением к коллекции
        /// </summary>      
        public static async Task<IResultCollection<TValueOut>> ResultValueContinueToCollectionAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                                                        Func<TValueIn, bool> predicate,
                                                                                                                        Func<TValueIn, Task<IEnumerable<TValueOut>>> okFunc,
                                                                                                                        Func<TValueIn, Task<IEnumerable<IErrorResult>>> badFunc) =>
            await @this.
            ResultValueContinueAsync(predicate, okFunc, badFunc).
            ToResultCollectionTaskAsync();

        /// <summary>
        /// Выполнение асинхронного положительного или негативного условия в результирующем ответе значения с возвращением к коллекции
        /// </summary>      
        public static async Task<IResultCollection<TValueOut>> ResultValueOkBadToCollectionAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                    Func<TValueIn, Task<IEnumerable<TValueOut>>> okFunc,
                                                                                    Func<IReadOnlyCollection<IErrorResult>, Task<IEnumerable<TValueOut>>> badFunc) =>
            await @this.
            ResultValueOkBadAsync(okFunc, badFunc).
            ToResultCollectionTaskAsync();

        /// <summary>
        /// Выполнение асинхронного положительного условия или возвращение предыдущей ошибки в результирующем ответе значения с возвращением к коллекции
        /// </summary>   
        public static async Task<IResultCollection<TValueOut>> ResultValueOkToCollectionAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                             Func<TValueIn, Task<IEnumerable<TValueOut>>> okFunc) =>
            await  @this.
            ResultValueOkAsync(okFunc).
            ToResultCollectionTaskAsync();
    }
}