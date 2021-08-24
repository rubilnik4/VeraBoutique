using System;
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
        /// Отсутствие ошибок
        /// </summary>
        bool OkStatus { get; }

        /// <summary>
        /// Присутствуют ли ошибки
        /// </summary>
        bool HasErrors { get; }

        /// <summary>
        /// Получить типы ошибок
        /// </summary>
        IReadOnlyCollection<Type> GetErrorTypes();

        /// <summary>
        /// Присутствует ли тип ошибки
        /// </summary>
        bool HasErrorType<TError>()
            where TError : struct;

        /// <summary>
        /// Получить ошибку
        /// </summary>      
        IErrorTypeResult<TError>? GetError<TError>()
            where TError : struct;

        /// <summary>
        /// Получить ошибку
        /// </summary>      
        IReadOnlyCollection<IErrorTypeResult<TError>> GetErrors<TError>()
            where TError : struct;

        /// <summary>
        /// Добавить ошибку
        /// </summary>      
        IResultError AppendError<TError>(IErrorResult error)
            where TError : struct;

        /// <summary>
        /// Добавить ошибки
        /// </summary>      
        IResultError ConcatErrors(IEnumerable<IErrorResult> errors);
    }
}