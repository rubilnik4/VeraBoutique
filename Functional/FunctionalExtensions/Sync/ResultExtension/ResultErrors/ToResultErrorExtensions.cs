using System.Collections.Generic;
using System.Linq;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Sync.ResultExtension.ResultErrors
{
    /// <summary>
    /// Преобразование в результирующий ответ
    /// </summary>  
    public static class ToResultErrorExtensions
    {
        /// <summary>
        /// Преобразовать в результирующий ответ
        /// </summary>  
        public static IResultError ToResultError(this IEnumerable<IResultError> @this) =>
            new ResultError(@this.SelectMany(result => result.Errors));
    }
}