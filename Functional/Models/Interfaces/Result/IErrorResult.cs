using System;
using System.Collections.Generic;
using Functional.Models.Enums;

namespace Functional.Models.Interfaces.Result
{
    /// <summary>
    /// Ошибка результирующего ответа
    /// </summary>
    public interface IErrorResult : IEnumerable<IErrorResult>
    {
        /// <summary>
        /// Тип ошибки при конвертации файлов
        /// </summary>
        ErrorResultType ErrorResultType { get; }

        /// <summary>
        /// Описание ошибки
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Исключение
        /// </summary>
        Exception? Exception { get; }

        /// <summary>
        /// Преобразовать в ответ с вложенным типом
        /// </summary>      
        IResultValue<TValue> ToResultValue<TValue>();
    }
}