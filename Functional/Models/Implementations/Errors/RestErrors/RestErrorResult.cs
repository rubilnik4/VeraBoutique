using System;
using Functional.Models.Enums;
using Functional.Models.Implementations.Errors.Base;
using Functional.Models.Implementations.Errors.CommonErrors;
using Functional.Models.Interfaces.Errors;
using Functional.Models.Interfaces.Errors.Base;

namespace Functional.Models.Implementations.Errors.RestErrors
{
    /// <summary>
    /// Ошибка сервера
    /// </summary>
    public class RestErrorResult : ErrorBaseResult<RestErrorType>
    {
        public RestErrorResult(RestErrorType restErrorType, string host, string description)
            : this(restErrorType, host, description, null)
        { }

        protected RestErrorResult(RestErrorType restErrorType, string host, string description, Exception? exception)
            : base(restErrorType, description, exception)
        {
            Host = host;
        }

        /// <summary>
        /// Адрес сервера
        /// </summary>
        public string Host { get; }

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override IErrorResult Initialize(string description, Exception? exception) =>
            new RestErrorResult(ErrorType, Host, description, exception);
    }
}