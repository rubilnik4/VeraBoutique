using System;
using System.Collections.Generic;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Sync.ResultExtension
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением
    /// </summary>
    public static class ResultValueExtensions
    {
        /// <summary>
        /// Преобразовать в результирующий ответ с  коллекцией
        /// </summary>      
        public static IResultCollection<TValue> ToResultCollection<TValue>(this IResultValue<IEnumerable<TValue>> @this) =>
            @this.OkStatus
                ? new ResultCollection<TValue>(@this.Value)
                : new ResultCollection<TValue>(@this.Errors);
    }
}