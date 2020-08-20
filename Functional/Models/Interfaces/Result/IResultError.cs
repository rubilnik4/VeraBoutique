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
        IReadOnlyCollection<IErrorResult> Errors { get; }

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
    }
}