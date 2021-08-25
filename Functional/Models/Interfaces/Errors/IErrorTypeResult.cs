using System;

namespace Functional.Models.Interfaces.Errors
{
    /// <summary>
    /// Ошибка результирующего ответа
    /// </summary>
    public interface IErrorTypeResult<out TError> : IErrorResult, IFormattable
        where TError : struct
    {
        /// <summary>
        /// Тип ошибки при конвертации файлов
        /// </summary>
        TError ErrorType { get; }
    }
}