using System;
using Functional.Models.Enums;
using Functional.Models.Implementations.Errors.Base;
using Functional.Models.Implementations.Errors.CommonErrors;
using Functional.Models.Interfaces.Errors;

namespace Functional.Models.Implementations.Errors.ConvertionErrors
{
    public class ConvertionErrorResult<TValue> : ErrorTypeResult<ConvertionErrorType>, IErrorTypeResult<ConvertionErrorType>
       where TValue : class
    {
        public ConvertionErrorResult(ConvertionErrorType convertionErrorType, TValue value, string description)
          : this(convertionErrorType, value, description, null)
        { }

        protected ConvertionErrorResult(ConvertionErrorType convertionErrorType, TValue value, string description, Exception? exception)
            : base(convertionErrorType, description, exception)
        {
            Value = value;
        }

        /// <summary>
        /// Конвертируемый параметр
        /// </summary>
        public TValue Value { get; }

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override IErrorResult Initialize(string description, Exception? exception) =>
            new CommonErrorResult(ErrorType, description, exception);
    }
}