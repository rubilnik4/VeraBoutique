using System.Collections.Generic;
using System.Linq;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection
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
                                                                                    IErrorResult errorNull)
            where TValue : class =>
            @this != null
                ? @this.Select(value => value.ToResultValueNullCheck(errorNull)).
                        ToResultCollection()
                : new ResultCollection<TValue>(errorNull);
    }
}