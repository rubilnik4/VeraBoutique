using System;
using Functional.Models.Enums;
using Functional.Models.Implementations.Errors.Base;
using Functional.Models.Implementations.Errors.CommonErrors;
using Functional.Models.Interfaces.Errors;
using Functional.Models.Interfaces.Errors.Base;

namespace Functional.Models.Implementations.Errors.ConvertionErrors
{
    /// <summary>
    /// Ошибка сериализации
    /// </summary>
    public class SerializeErrorResult<TValue> : ErrorBaseResult<ConvertionErrorType>
       where TValue : notnull
    {
        public SerializeErrorResult(ConvertionErrorType convertionErrorType, TValue value, string description)
          : this(convertionErrorType, value, description, null)
        { }

        protected SerializeErrorResult(ConvertionErrorType convertionErrorType, TValue value, string description, Exception? exception)
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
            new SerializeErrorResult<TValue>(ErrorType, Value, description, exception);
    }
}