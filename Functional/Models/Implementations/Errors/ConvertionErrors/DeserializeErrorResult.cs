using System;
using Functional.Models.Enums;
using Functional.Models.Implementations.Errors.Base;
using Functional.Models.Interfaces.Errors;
using Functional.Models.Interfaces.Errors.Base;

namespace Functional.Models.Implementations.Errors.ConvertionErrors
{
    /// <summary>
    /// Ошибка десериализации
    /// </summary>
    public class DeserializeErrorResult<TValue> : ErrorBaseResult<ConvertionErrorType>
       where TValue : notnull
    {
        public DeserializeErrorResult(ConvertionErrorType convertionErrorType, string value, string description)
            : this(convertionErrorType, value, description, null)
        { }

        protected DeserializeErrorResult(ConvertionErrorType convertionErrorType, string value, string description, Exception? exception)
            : base(convertionErrorType, description, exception)
        {
            Value = value;
        }

        /// <summary>
        /// Конвертируемый параметр
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Тип конвертации
        /// </summary>
        public Type DeserializeType => 
            typeof(TValue);

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override IErrorResult Initialize(string description, Exception? exception) =>
            new DeserializeErrorResult<TValue>(ErrorType, Value, description, exception);
    }
}