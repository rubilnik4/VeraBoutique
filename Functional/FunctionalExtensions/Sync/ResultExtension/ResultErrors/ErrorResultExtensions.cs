using System;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Sync.ResultExtension.ResultErrors
{
    /// <summary>
    /// Преобразование в результирующую ошибку
    /// </summary>
    public static class ToErrorResultExtensions
    {
        /// <summary>
        /// Преобразовать в результирующую ошибку
        /// </summary>
        public static IErrorTypeResult<TError> ToErrorTypeResult<TError>(this TError error, string description)
            where TError : struct =>
            new ErrorTypeResult<TError>(error, description);
    }
}