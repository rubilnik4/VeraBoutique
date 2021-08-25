using System;
using Functional.Models.Enums;
using Functional.Models.Implementations.Errors.Base;
using Functional.Models.Implementations.Errors.CommonErrors;
using Functional.Models.Interfaces.Errors;

namespace Functional.Models.Implementations.Errors.AuthorizeErrors
{
    /// <summary>
    /// Ошибка при авторизации
    /// </summary>
    public class AuthorizeErrorResult : ErrorTypeResult<AuthorizeErrorType>
    {
        public AuthorizeErrorResult(AuthorizeErrorType authorizeErrorType, string description)
            : this(authorizeErrorType, description, null)
        { }

        protected AuthorizeErrorResult(AuthorizeErrorType authorizeErrorType, string description, Exception? exception)
            : base(authorizeErrorType, description, exception)
        { }

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override IErrorResult Initialize(string description, Exception? exception) =>
            new AuthorizeErrorResult(ErrorType, description, exception);
    }
}