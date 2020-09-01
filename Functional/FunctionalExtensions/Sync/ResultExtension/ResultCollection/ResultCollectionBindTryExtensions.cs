using System;
using System.Collections.Generic;
using Functional.Models.Interfaces.Result;
using static Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection.ResultCollectionTryExtensions;

namespace Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection
{
    /// <summary>
    /// Методы расширения для результирующего ответа с коллекцией и обработкой исключений
    /// </summary>
    public static class ResultCollectionBindTryExtensions
    {
        /// <summary>
        /// Связать результирующий ответ с коллекцией с обработкой функции при положительном условии
        /// </summary>
        public static IResultCollection<TValueOut> ResultCollectionBindTryOk<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                        Func<IReadOnlyCollection<TValueIn>, IEnumerable<TValueOut>> func,
                                                                                        IErrorResult error) =>
            @this.ResultCollectionBindOk(value => ResultCollectionTry(() => func.Invoke(value), error));
    }
}