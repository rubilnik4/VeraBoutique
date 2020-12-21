using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Functional.Models.Enums;
using Functional.Models.Interfaces.Result;

namespace Functional.Models.Implementations.Result
{
    /// <summary>
    /// Ошибка результирующего ответа
    /// </summary>
    public class ErrorResult: IErrorResult, IFormattable
    {
        public ErrorResult(ErrorResultType errorResultType, string description)
          : this(errorResultType, description, null) { }

        public ErrorResult(ErrorResultType errorResultType, string description, Exception? exception)
        {
            ErrorResultType = errorResultType;
            Description = description;
            Exception = exception;
        }

        /// <summary>
        /// Тип ошибки при конвертации файлов
        /// </summary>
        public ErrorResultType ErrorResultType { get; }

        /// <summary>
        /// Описание ошибки
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Исключение
        /// </summary>
        public Exception? Exception { get; }

        /// <summary>
        /// Добавить или заменить исключение
        /// </summary>
        public IErrorResult AppendException(Exception exception) =>
            new ErrorResult(ErrorResultType, Description, exception);

        /// <summary>
        /// Преобразовать в ответ
        /// </summary>      
        public IResultError ToResult() => new ResultError(this);

        /// <summary>
        /// Преобразовать в ответ с вложенным типом
        /// </summary>      
        public IResultValue<TValue> ToResultValue<TValue>() => new ResultValue<TValue>(this);

        /// <summary>
        /// Преобразовать в ответ с вложенной коллекцией
        /// </summary>      
        public IResultCollection<TValue> ToResultCollection<TValue>() => new ResultCollection<TValue>(this);

        #region IEnumerable Support
        /// <summary>
        /// Реализация перечисления
        /// </summary>       
        public IEnumerator<IErrorResult> GetEnumerator()
        {
            yield return this;
        }

        /// <summary>
        /// Реализация перечисления
        /// </summary>  
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        #endregion

        #region IFormattable Support
        public override string ToString() => ToString(String.Empty, CultureInfo.CurrentCulture);

        public string ToString(string? format, IFormatProvider? formatProvider) => ErrorResultType.ToString();
        #endregion
    }
}