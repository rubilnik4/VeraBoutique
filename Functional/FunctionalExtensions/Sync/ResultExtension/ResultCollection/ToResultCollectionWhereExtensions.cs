using System;
using System.Collections.Generic;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection
{
    /// <summary>
    /// Преобразование значений в результирующий ответ коллекции с условием
    /// </summary>
    public static class ToResultCollectionWhereExtensions
    {
        /// <summary>
        /// Преобразовать значений в результирующий ответ коллекции с условием
        /// </summary>
        public static IResultCollection<TValue> ToResultCollectionWhere<TValue>(this IEnumerable<TValue> @this,
                                                                                Func<IEnumerable<TValue>, bool> predicate,
                                                                                Func<IEnumerable<TValue>, IErrorResult> badFunc)
            where TValue : notnull =>
          @this.WhereContinue(predicate,
                              values => (IResultCollection<TValue>)new ResultCollection<TValue>(values),
                              values => new ResultCollection<TValue>(badFunc(values)));
    }
}