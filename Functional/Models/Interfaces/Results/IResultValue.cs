using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Functional.Models.Interfaces.Errors;
using Functional.Models.Interfaces.Errors.Base;

namespace Functional.Models.Interfaces.Results
{
    /// <summary>
    /// Базовый вариант ответа со значением
    /// </summary>
    public interface IResultValue<out TValue>: IResultError
    {
        /// <summary>
        /// Значение
        /// </summary>
        [AllowNull]
        TValue Value { get; }

        /// <summary>
        /// Добавить ошибку
        /// </summary>      
        new IResultValue<TValue> ConcatErrors(IEnumerable<IErrorResult> errors);
    }
}