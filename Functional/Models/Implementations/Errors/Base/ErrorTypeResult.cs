using System;
using System.Globalization;
using Functional.Models.Interfaces.Errors;

namespace Functional.Models.Implementations.Errors.Base
{
    /// <summary>
    /// Ошибка результирующего ответа
    /// </summary>
    public abstract class ErrorTypeResult<TError> : ErrorResult, IErrorTypeResult<TError>
        where TError : struct
    {
        protected ErrorTypeResult(TError errorType, string description)
            : this(errorType, description, null) 
        { }

        protected ErrorTypeResult(TError errorType, string description, Exception? exception)
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

        #region IFormattable Support
        public override string ToString() =>
            ToString(String.Empty, CultureInfo.CurrentCulture);

        public string ToString(string? format, IFormatProvider? formatProvider) =>
            ErrorType.ToString();
        #endregion
    }
}