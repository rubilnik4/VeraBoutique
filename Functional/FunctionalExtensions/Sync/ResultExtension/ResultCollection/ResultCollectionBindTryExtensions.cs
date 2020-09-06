using System;
using System.Collections.Generic;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using static Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection.ResultCollectionTryExtensions;

namespace Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection
{
    /// <summary>
    /// Методы расширения для результирующего ответа со связыванием с коллекцией и обработкой исключений
    /// </summary>
    public static class ResultCollectionBindTryExtensions
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ со связыванием со значением или ошибку исключения
        /// </summary>
        public static IResultCollection<TValue> ResultCollectionBindTry<TValue>(Func<IResultCollection<TValue>> func, IErrorResult error)
        {
            IResultCollection<TValue> funcResult;

            try
            {
                funcResult = func.Invoke();
            }
            catch (Exception ex)
            {
                return new ResultCollection<TValue>(error.AppendException(ex));
            }

            return funcResult;
        }

        /// <summary>
        /// Связать результирующий ответ со значением со связыванием с обработкой функции при положительном условии
        /// </summary>
        public static IResultCollection<TValueOut> ResultCollectionBindTryOk<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                             Func<IReadOnlyCollection<TValueIn>, IEnumerable<TValueOut>> func,
                                                                                             IErrorResult error) =>
            @this.ResultCollectionBindOk(value => ResultCollectionBindTry(() => func.Invoke(value), error));
    }
}