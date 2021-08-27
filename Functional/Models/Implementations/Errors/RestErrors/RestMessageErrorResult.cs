using System;
using System.Net.Http;
using Functional.Models.Enums;
using Functional.Models.Implementations.Errors.Base;
using Functional.Models.Interfaces.Errors;
using Functional.Models.Interfaces.Errors.Base;

namespace Functional.Models.Implementations.Errors.RestErrors
{
    /// <summary>
    /// Ошибка сервера с сообщением
    /// </summary>
    public class RestMessageErrorResult : ErrorBaseResult<RestErrorType>
    {
        public RestMessageErrorResult(RestErrorType restErrorType, HttpResponseMessage httpResponseMessage, string description)
            : this(restErrorType, httpResponseMessage, description, null)
        { }

        protected RestMessageErrorResult(RestErrorType restErrorType, HttpResponseMessage httpResponseMessage, string description, Exception? exception)
            : base(restErrorType, description, exception)
        {
            HttpResponseMessage = httpResponseMessage;
        }

        /// <summary>
        /// Адрес сервера
        /// </summary>
        public HttpResponseMessage HttpResponseMessage { get; }

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override IErrorResult Initialize(string description, Exception? exception) =>
            new RestMessageErrorResult(ErrorType, HttpResponseMessage, description, exception);
    }
}