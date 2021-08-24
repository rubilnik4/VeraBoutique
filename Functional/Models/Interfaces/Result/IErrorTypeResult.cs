using System;
using System.Collections.Generic;
using Functional.Models.Enums;

namespace Functional.Models.Interfaces.Result
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