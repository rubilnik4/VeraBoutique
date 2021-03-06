﻿using System;
using System.Collections.Generic;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection
{
    /// <summary>
    /// Методы расширения для результирующего ответа со коллекцией и обработкой исключений
    /// </summary>
    public static class ResultCollectionTryExtensions
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с коллекцией или ошибку исключения
        /// </summary>
        public static IResultCollection<TValue> ResultCollectionTry<TValue>(Func<IEnumerable<TValue>> func,
                                                                            IErrorResult error)
        {
            IEnumerable<TValue> funcCollectionResult;

            try
            {
                funcCollectionResult = func.Invoke();
            }
            catch (Exception ex)
            {
                return new ResultCollection<TValue>(error.AppendException(ex));
            }

            return new ResultCollection<TValue>(funcCollectionResult);
        }

        /// <summary>
        /// Связать результирующий ответ с коллекцией с обработкой функции при положительном условии
        /// </summary>
        public static IResultCollection<TValueOut> ResultCollectionTryOk<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                  Func<IReadOnlyCollection<TValueIn>, IEnumerable<TValueOut>> func,
                                                                                                  IErrorResult error) =>
            @this.ResultCollectionBindOk(value => ResultCollectionTry(() => func.Invoke(value), error));
    }
}