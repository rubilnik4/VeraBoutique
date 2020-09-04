﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений асинхронно
    /// </summary>
    public static class ResultCollectionTryAsyncExtensions
    {
        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением или ошибку исключения
        /// </summary>
        public static async Task<IResultCollection<TValue>> ResultCollectionTryAsync<TValue>(Func<Task<IEnumerable<TValue>>> func, 
                                                                                             IErrorResult error)
        {
            IEnumerable<TValue> funcCollectionResult;

            try
            {
                funcCollectionResult = await func.Invoke();
            }
            catch (Exception ex)
            {
                
                return new ResultCollection<TValue>(error.AppendException(ex));
            }

            return new ResultCollection<TValue>(funcCollectionResult);
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением или ошибку исключения
        /// </summary>
        public static async Task<IResultCollection<TValue>> ResultCollectionTryAsync<TValue>(Func<Task<IReadOnlyCollection<TValue>>> func,
                                                                                             IErrorResult error) =>
            await ResultCollectionTryAsync(async () => (IEnumerable<TValue>)await func.Invoke(), error);

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением или ошибку исключения
        /// </summary>
        public static async Task<IResultCollection<TValue>> ResultCollectionTryAsync<TValue>(Func<Task<List<TValue>>> func,
                                                                                             IErrorResult error) =>
            await ResultCollectionTryAsync(async () => (IEnumerable<TValue>)await func.Invoke(), error);
    }
}