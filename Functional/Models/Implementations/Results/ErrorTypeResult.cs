using System;
using System.Globalization;
using Functional.Models.Enums;
using Functional.Models.Interfaces.Result;

namespace Functional.Models.Implementations.Results
{
    /// <summary>
    /// Ошибка результирующего ответа
    /// </summary>
    public class ErrorTypeResult<TError> : ErrorResult, IErrorTypeResult<TError>
        where TError : struct
    {
        public ErrorTypeResult(TError errorType, string description)
            : this(errorType, description, null) { }

        public ErrorTypeResult(TError errorType, string description, Exception? exception)
            : base(description, exception)
        {
            ErrorType = errorType;
        }

        /// <summary>
        /// Тип ошибки при конвертации файлов
        /// </summary>
        public TError ErrorType { get; }

        /// <summary>
        /// Наличие типа ошибки
        /// </summary>
        public override bool HasErrorType<TErrorType>()
            where TErrorType : struct =>
            typeof(TError) == typeof(TErrorType);

        /// <summary>
        /// Добавить или заменить исключение
        /// </summary>
        public override IErrorResult AppendException(Exception exception) =>
            new ErrorTypeResult<TError>(ErrorType, Description, Exception);

        #region IFormattable Support
        public override string ToString() =>
            ToString(String.Empty, CultureInfo.CurrentCulture);

        public string ToString(string? format, IFormatProvider? formatProvider) =>
            ErrorType.ToString();
        #endregion
    }
}