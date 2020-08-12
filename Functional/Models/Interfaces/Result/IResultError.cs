using System.Collections.Generic;

namespace Functional.Models.Interfaces.Result
{
    /// <summary>
    /// Базовый вариант ответа
    /// </summary>
    public interface IResultError
    {
        /// <summary>
        /// Список ошибок
        /// </summary>
        IReadOnlyList<IErrorResult> Errors { get; }

        /// <summary>
        /// Присутствуют ли ошибки
        /// </summary>
        bool HasErrors { get; }

        /// <summary>
        /// Отсутствие ошибок
        /// </summary>
        bool OkStatus { get; }

        /// <summary>
        /// Добавить ошибку
        /// </summary>      
        IResultError ConcatErrors(IEnumerable<IErrorResult> errors);

        /// <summary>
        /// Преобразовать в результирующий ответ со значением
        /// </summary>
        IResultValue<TValue> ToResultValue<TValue>(TValue value) where TValue : notnull;
    }
}