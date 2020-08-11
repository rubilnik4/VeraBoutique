using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Functional.Models.Interfaces.Result
{
    /// <summary>
    /// Базовый вариант ответа со значением
    /// </summary>
    public interface IResultValue<out TValue>
    {
        /// <summary>
        /// Список значений
        /// </summary>
        [MaybeNull]
        TValue Value { get; }

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
        IResultValue<TValue> ConcatErrors(IEnumerable<IErrorResult> errors);

        /// <summary>
        /// Преобразовать в результирующий тип
        /// </summary>
        IResultError ToResult();
    }
}