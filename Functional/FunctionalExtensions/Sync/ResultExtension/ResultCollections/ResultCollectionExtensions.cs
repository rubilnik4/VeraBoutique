using System.Collections.Generic;
using System.Linq;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollections
{
    /// <summary>
    /// Методы расширения для результирующей коллекции со значением
    /// </summary>
    public static class ResultCollectionExtensions
    {
        /// <summary>
        /// Преобразовать значение в результирующий ответ коллекции с проверкой на нуль
        /// </summary>
        public static IResultCollection<TValue> ToResultCollectionNullCheck<TValue>(this IEnumerable<TValue?>? @this,
                                                                                    IErrorResult error)
            where TValue : class =>
            @this != null
                ? @this.Select(value => value.ToResultValueNullCheck(error)).
                        ToResultCollection()
                : new ResultCollection<TValue>(error);
    }
}