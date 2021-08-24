using System;
using System.Collections.Generic;

namespace Functional.Models.Interfaces.Result
{
    /// <summary>
    /// Ошибка результирующего ответа. Базовый класс
    /// </summary>
    public interface IErrorResult : IEnumerable<IErrorResult>
    {
        /// <summary>
        /// Описание ошибки
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Исключение
        /// </summary>
        Exception? Exception { get; }

        /// <summary>
        /// Наличие типа ошибки
        /// </summary>
        bool HasErrorType<TError>()
            where TError: struct;

        /// <summary>
        /// Добавить или заменить исключение
        /// </summary>
        IErrorResult AppendException(Exception exception);

        /// <summary>
        /// Преобразовать в ответ
        /// </summary>      
        IResultError ToResult();

        /// <summary>
        /// Преобразовать в ответ с вложенным типом
        /// </summary>      
        IResultValue<TValue> ToResultValue<TValue>();

        /// <summary>
        /// Преобразовать в ответ с вложенной коллекцией
        /// </summary>      
        IResultCollection<TValue> ToResultCollection<TValue>();
    }
}